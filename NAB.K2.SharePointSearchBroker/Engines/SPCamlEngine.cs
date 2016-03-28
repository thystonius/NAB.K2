#region Copyright (c) 2015-2016 Nathan A. Brown
/**********************************************************************************
*   Copyright (C) 2015-2016 Nathan A. Brown - nab@thystonius.com
*   This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
*   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/ or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
*   All other rights reserved.
*   This work is considered proprietary.  Any use or right not covered in above license is considered reserved.
*   Use of this work or any derivative constitute an acceptance of all license terms and conditions.
*   
*   Project: NAB.K2.SharePointSearch
*   Namespace: NAB.K2.SharePointSearch.Engines
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Runtime.Caching;

using Microsoft.SharePoint.Client;
using SPQuery = Microsoft.SharePoint.Client.Search.Query;

using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;
using System.Threading;

namespace NAB.K2.SharePointSearch.Engines
{


    public class SPCamlEngine : IQueryEngine
    {




        #region Retrieve Columns
        public List<ReturnColumn> RetrieveOutputColumns(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime qRuntime)
        {
            List<ReturnColumn> cols = new List<ReturnColumn>();

            using (ClientContext clientContext = new ClientContext(qRuntime.SiteUrl.GenerateString(parameters)))
            {

                //Execute the query
                ListItemCollection l = _ExecuteQuery(conn, clientContext, parameters, qRuntime);

                if (l.Count > 0)
                {
                    //Get the first item
                    var i = l[0];

                    //Get all fields for the list (so we can lookup what we found)
                    FieldCollection fc = i.ParentList.Fields;
                    clientContext.Load(fc);
                    //clientContext.Load(fc);
                    clientContext.Load(fc, f => f.Include(fd => fd.Title, fd => fd.Description, fd => fd.InternalName, fd => fd.StaticName, fd => fd.TypeAsString));
                    clientContext.ExecuteQuery();

                    //Ok, this is kinda silly, but we have to put all these fields into a dictionary so we can find them quickly
                    Dictionary<string, Field> fields = fc.ToDictionary(f => f.InternalName, StringComparer.InvariantCultureIgnoreCase);

                    
                    ReturnColumn rc;
                    foreach(var curCol in i.FieldValues.Keys)
                    {
                        //Create the new return column
                        rc = new ReturnColumn();
                        
                        //Try and lookup this column in the "full" list of columns for this list
                        
                        if (fields.ContainsKey(curCol))
                        {
                            var colMeta = fields[curCol];

                            //We found this column, use the metadata to build better column definition
                            rc.Name = colMeta.InternalName;
                            try
                            {
                                rc.DisplayName = colMeta.Title;
                                rc.Description = colMeta.Description;
                            }
                            catch (Exception)
                            {
                                rc.DisplayName = colMeta.InternalName;
                                rc.Description = colMeta.InternalName;
                            }
                            
                            //Thi is VERY critical to get correct.  K2 Uses this value to build DataTables for your Service Objects (so be sure to use proper .NET Types if you want things to work!!)
                            rc.ColumnType = TypeMapper.DecipherObjectType(i.FieldValues[colMeta.InternalName]);
                            rc.InternalType = rc.ColumnType.Name;


                        }else{
                            //Not sure where this column comes from, but it is in the result list so add it
                            rc.Name = curCol;
                            rc.DisplayName = curCol;
                            rc.ColumnType = TypeMapper.DecipherObjectType(i.FieldValues[curCol]);

                            rc.InternalType = rc.ColumnType.Name;
                            
                        }

                        rc.ContainsData = true;
                                                

                        cols.Add(rc);
                    }


                    return cols;
                }
                else
                {
                    //When no rows returned - so we can't detect any columns
                    return cols;
                }

            }

        } 
        #endregion
        
        #region Execute Methods
        public bool ExecuteToK2(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime qRuntime, ServiceAssemblyBase broker, ServiceObject serviceObject, Property[] returnProperties, CancellationToken cancelToken)
        {


            using (ClientContext clientContext = new ClientContext(qRuntime.SiteUrl.GenerateString(parameters)))
            {

                //Execute the query
                ListItemCollection l = _ExecuteQuery(conn, clientContext, parameters, qRuntime, true);

                //Initialize Results Table
                serviceObject.Properties.InitResultTable();
                DataTable dt = broker.ServicePackage.ResultTable;

                int rowCount = 0;

                //Check for cancelation
                cancelToken.ThrowIfCancellationRequested();

                DataRow dr;
                Property p;

                //Loop through the items in the collection
                foreach (var item in l)
                {
                    dr = dt.NewRow();
                    
                    //Get each return property
                    for (int i = 0; i < returnProperties.Length; i++)
                    {
                        //Current return column
                        p = returnProperties[i];
        
                        dr[p.Name] = item.FieldValues[qRuntime.GetReturnName(p.Name)] ?? DBNull.Value;
                        
                    }

                    //Add the row
                    dt.Rows.Add(dr);

                    rowCount++;
                    if (rowCount > conn.MaxRows || rowCount > qRuntime.Query.MaxRecords)
                    {
                        //Hit max records
                        break;
                    }
                    
                    //Check for cancelation
                    cancelToken.ThrowIfCancellationRequested();


                }

            }

            return true;

        }




        public DataTable ExecuteToDataTable(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime qRuntime)
        {

            List<ReturnColumn> cols = new List<ReturnColumn>();

            using (ClientContext clientContext = new ClientContext(qRuntime.SiteUrl.GenerateString(parameters)))
            {

                //Execute the query
                ListItemCollection l = _ExecuteQuery(conn, clientContext, parameters, qRuntime);

                DataTable dt = qRuntime.Query.BuildDataTable();

                int rowCount = 0;

                if (true && l.Count > 0)
                {
                    //Get the first item
                    foreach (var item in l)
                    {
                        DataRow dr = dt.Rows.Add();

                        foreach (var col in qRuntime.Query.Columns)
                        {
                            dr[col.Name] = item.FieldValues[col.SourceColumn];

                        }

                        rowCount++;

                        if (rowCount > conn.MaxRows || rowCount > qRuntime.Query.MaxRecords)
                        {
                            //Hit max records
                            break;
                        }
                    }

                    return dt;
                }
                else
                {
                    //When no rows returned - so we can't detect any columns
                    return dt;
                }

            }

        }



        private ListItemCollection _ExecuteQuery(IRuntimeConnection conn, ClientContext context, IMacroValueProvider parameters, QueryRuntime qRuntime, bool allowCaching = false)
        {

            //Setup
            string cacheKey = null;
            string viewXml = qRuntime.QueryText.GenerateString(parameters);
            string listUrl = qRuntime.FullUrl.GenerateString(parameters);

            if (allowCaching)
            {
                //Calculate has based on list url and CAML
                UInt64 h = HashUtility.CalculateHash(viewXml);
                h += HashUtility.CalculateHash(listUrl);

                //Retrive a key based on the runtime
                cacheKey = qRuntime.CacheGenerateId(h);

                ListItemCollection data = qRuntime.CacheRetrieve(cacheKey) as ListItemCollection;

                //If we hit then return and be on our way
                if (data != null)
                {
                    return data;
                }

            }


            //Have the connective provide authentication informaiton
            conn.AuthenticateContext(context);

            //Build CAML
            CamlQuery caml = new CamlQuery();

            caml.ViewXml = viewXml;
            caml.DatesInUtc = true;
            
            //Get the list
            List list = context.Web.GetList(listUrl);
            
            //Queue up the search
            ListItemCollection l = list.GetItems(caml);

            //Queue the loading of the list
            context.RequestTimeout = conn.MaxTimeout * 1000;
            context.Load(l);
            context.ExecuteQuery();

            //Store this, if we allow caching
            if (allowCaching)
            {
                qRuntime.CacheStore(cacheKey, l);
            }

            return l;

        } 
        #endregion
        

        #region Service Object Creation
        public List<ServiceObject> CreateServiceObjects(ServiceInstance service, QueryDef query)
        {

            ServiceObject so = service.ServiceObjects.Create(query.QueryId);
            so.MetaData.DisplayName = query.DisplayName;
            so.MetaData.Description = query.Description;

            //Columns get added as Properties
            Property prop;
            foreach (var col in query.Columns)
            {
                //Only include those marked as such
                if (col.Include)
                {
                    //NOTE - Column Names must be kept in the same case as specified as these are the internal names from provider
                    prop = so.Properties.Create(col.Name);

                    //Display name and decsriptions can be in any case (who cares)
                    prop.MetaData.DisplayName = col.DisplayName;
                    prop.MetaData.Description = col.Description;

                    //WARNING, this Assumes that the SoType enum will not get re-numbered by K2 / SourceCode
                    prop.SoType = (SoType)col.SMOType;
                    //prop.Type = TypeMapper.GetTypeForSO((NABK2SoType)col.SMOType).Name.ToLower();
                    prop.Type = "System." + col.ColumnType;
                    prop.ExtendType = "default";

                }
            }


            //Then there is a single List method with all the parameters
            //////////////////////////////////////////////////////////////////////////
            //LIST Method
            Method method = so.Methods.Create("List");
            method.MetaData.DisplayName = "List " + query.DisplayName;
            method.MetaData.Description = query.Description;
            method.Type = MethodType.List;


            //No input properties for list


            //All Properties are for the return
            ReturnProperties returnProps = new ReturnProperties();
            foreach (Property p in so.Properties)
            {
                returnProps.Add(p);
            }
            method.ReturnProperties = returnProps;


            //Now all the query parameters
            MethodParameter param;

            foreach (var p in query.Parameters)
            {
                //Only for non-calculated columns
                if (!p.IsCalculated)
                {
                    //NOTE - All Parameters are forced ToUpper to prevent from needing to do this everywhere
                    param = method.MethodParameters.Create(p.Name.ToUpper());
                    param.MetaData.DisplayName = p.DisplayName;
                    param.MetaData.Description = p.Description;

                    //WARNING - this assumes that the SoTypes Enum will not be changed by K2
                    param.SoType = (SoType)p.SMOType;

                    //Currently all parameters are required
                    param.IsRequired = true;

                }

            }

            //Build a list of just one item
            List<ServiceObject> list = new List<ServiceObject>();
            list.Add(so);

            return list;


        } 
        #endregion
        

    }
}
