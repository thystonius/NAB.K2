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
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;

using Microsoft.SharePoint.Client;
using SPSearch = Microsoft.SharePoint.Client.Search.Query;

using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;

namespace NAB.K2.SharePointSearch.Engines
{


    public class SPSearchEngine : IQueryEngine
    {
        /// <summary>
        /// Suffix added to QueryID for refiners (DO NOT CHANGE THIS IF YOU HAVE REFINER SMOs, as that will cause those SMOs to not be found).
        /// </summary>
        public static string REFINER_SUFFIX = ".REFINERS";
        
        public static string REFINER_PARAMETER = "SYS.REFINER";

        public static string METADATA_REFINERSEARCH = "REFINER-SEARCH";


        //Refiner Columns
        public static string PROP_REFINERNAME = "RefinerName";
        public static string PROP_TYPE = "Type";
        public static string PROP_SCORE = "Score";
        public static string PROP_HITCOUNT = "HitCount";
        public static string PROP_MIN = "Min";
        public static string PROP_MAX = "Max";
        public static string PROP_MEAN = "Mean";
        public static string PROP_REFINEMENT_NAME = "RefinementName";
        public static string PROP_REFINEMENT_DISPLAY = "RefinementDisplay";
        public static string PROP_REFINEMENT_TOKEN = "RefinementToken";
        public static string PROP_COUNT = "RefinementCount";


        #region Retrieve Columns
        public List<DetectedColumn> DetectOutputColumns(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime qRuntime)
        {
            List<DetectedColumn> cols = new List<DetectedColumn>();

            using (ClientContext clientContext = new ClientContext(qRuntime.SiteUrl.GenerateString(parameters)))
            {

                //Execute the query (only a single row is fine)
                SPSearch.ResultTable t = _ExecuteQuery(conn, clientContext, parameters, qRuntime, 1);

                if (true && t.RowCount > 0)
                {
                    //Get the first item
                    var i = t.ResultRows.First();

                    int valueLength;

                    foreach (var key in i.Keys)
                    {
                        Type colType = TypeMapper.DecipherObjectType(i[key]);
                        
                        //For strings we should at least try to determine if this is a memo item or not
                        valueLength = 0;
                        if (i[key] is string)
                        {
                            valueLength = ((string)i[key]).Length;
                        }

                        cols.Add(new DetectedColumn() { Name = key, DisplayName = key, Description = key, InternalType = colType.Name, ColumnType = colType, ContainsData = true, ContentLength = valueLength });

                        
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
            //If the Service Object Ends with the Refiner Suffix, then this is for refiness
            //Could have also looked at the metadata, but this is just as easy
            if (serviceObject.Name.EndsWith(REFINER_SUFFIX, StringComparison.InvariantCultureIgnoreCase))
            {
                //Execute as refiner search
                return ExecuteToK2_Refiner(conn, parameters, qRuntime, broker, serviceObject, returnProperties, cancelToken);
            }
            else
            {
                //Execute as search query
                return ExecuteToK2_Search(conn, parameters, qRuntime, broker, serviceObject, returnProperties, cancelToken);
            }


        }


        private bool ExecuteToK2_Search(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime qRuntime, ServiceAssemblyBase broker, ServiceObject serviceObject, Property[] returnProperties, CancellationToken cancelToken)
        {

            //keywordQuery.RefinementFilters.Add("FileType:" + refinementFilters[i]);
            
            using (ClientContext clientContext = new ClientContext(qRuntime.SiteUrl.GenerateString(parameters)))
            {
                
                //Execute the query
                SPSearch.ResultTable t = _ExecuteQuery(conn, clientContext, parameters, qRuntime, qRuntime.Query.MaxRecords);

                //Check for cancelation
                cancelToken.ThrowIfCancellationRequested();
                                                
                //Initialize Results Table
                serviceObject.Properties.InitResultTable();
                DataTable dt = broker.ServicePackage.ResultTable;

                int rowCount = 0;

                //Loop through the items in the collection
                foreach (var item in t.ResultRows)
                {

                    DataRow dr = dt.NewRow();

                    //Get each return property
                    for (int i = 0; i < returnProperties.Length; i++)
                    {
                        //Get the current return property
                        string returnName = returnProperties[i].Name;

                        //Set value (use DBNull in case of null)
                        dr[returnName] = item[qRuntime.GetReturnName(returnName)] ?? DBNull.Value;

                    }

                    //Add the row
                    dt.Rows.Add(dr);
                    
                    //Check for max row limit
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


        private bool ExecuteToK2_Refiner(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime qRuntime, ServiceAssemblyBase broker, ServiceObject serviceObject, Property[] returnProperties, CancellationToken cancelToken)
        {

            //keywordQuery.RefinementFilters.Add("FileType:" + refinementFilters[i]);


            //Now all the query parameters (Same as for the query, so the refiners will match)
            //ServiceObjectHelper.CreateMethodParameter(so, method, REFINER_PARAMETER, "Refiner Field", "Search Managed Property to get the list of refiner values for", SoType.Text, true);
            
            using (ClientContext clientContext = new ClientContext(qRuntime.SiteUrl.GenerateString(parameters)))
            {

                //Execute the query using the Refiner method so we only return the refiners
                SPSearch.ResultTable t = _ExecuteRefinerQuery(conn, clientContext, parameters, qRuntime, parameters.GetValueRaw(REFINER_PARAMETER));

                //Initialize Results Table
                serviceObject.Properties.InitResultTable();
                DataTable dt = broker.ServicePackage.ResultTable;

                int rowCount = 0;

                //Loop through the items in the collection
                foreach (var item in t.ResultRows)
                {

                    DataRow dr = dt.NewRow();

                    //We are in control of all of the columns so no need to loop through the return columns
                    //Just add them all (they better be there, or there has been an upgrade and the user did not refresh service instance)
                    dr[PROP_REFINERNAME] = item[PROP_REFINERNAME];
                    dr[PROP_TYPE] = item[PROP_TYPE];
                    dr[PROP_SCORE] = item[PROP_SCORE];
                    dr[PROP_HITCOUNT] = item[PROP_HITCOUNT];
                    dr[PROP_MIN] = item[PROP_MIN];
                    dr[PROP_MAX] = item[PROP_MAX];
                    dr[PROP_MEAN] = item[PROP_MEAN];
                    dr[PROP_REFINEMENT_NAME] = item[PROP_REFINEMENT_NAME];                    
                    dr[PROP_REFINEMENT_TOKEN] = item[PROP_REFINERNAME] + ":" + item[PROP_REFINEMENT_TOKEN];
                    dr[PROP_COUNT] = item[PROP_COUNT];

                    //Try and logically parse the display

                    dr[PROP_REFINEMENT_DISPLAY] = item[PROP_REFINEMENT_NAME];
                    

                    //Add the row
                    dt.Rows.Add(dr);

                    //Check for max row limit
                    rowCount++;
                    if (rowCount > conn.MaxRows || rowCount > qRuntime.Query.MaxRecords)
                    {
                        //Hit max records
                        break;
                    }

                }

            }

            return true;

        }



        public DataTable ExecuteToDataTable(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime qRuntime)
        {

            List<DetectedColumn> cols = new List<DetectedColumn>();

            using (ClientContext clientContext = new ClientContext(qRuntime.SiteUrl.GenerateString(parameters)))
            {

                //Execute the query
                SPSearch.ResultTable t = _ExecuteQuery(conn, clientContext, parameters, qRuntime, qRuntime.Query.MaxRecords);

                DataTable dt = qRuntime.Query.BuildDataTable();

                int rowCount = 0;

                if (true && t.RowCount > 0)
                {
                    //Get the first item
                    foreach (var item in t.ResultRows)
                    {
                        DataRow dr = dt.Rows.Add();

                        foreach (var col in qRuntime.ReturnColumns)
                        {
                            if(item.ContainsKey(col.Column.SourceColumn))
                            {
                                //dr[col.Name] = item[col.SourceColumn] ?? DBNull.Value;
                                dr[col.Column.Name] = _GetColumnValue(item, parameters, col);
                            }
                            else
                            {
                                throw new Exception(string.Format("Column {0} not found in result set.", col.Column.SourceColumn));
                            }                            
                            
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private object _GetColumnValue(IDictionary<string, object> resultRow, IMacroValueProvider parameters, ReturnColumn column)
        {
            if(column.IsProcessed)
            {
                return column.ProcessColumnOutput(resultRow[column.Column.SourceColumn], parameters);
            }
            else
            {
                return resultRow[column.Column.SourceColumn] ?? DBNull.Value;
            }

        }


        /// <summary>
        /// Internal method to execute the Search Query
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <param name="qRuntime"></param>
        /// <returns></returns>
        private SPSearch.ResultTable _ExecuteQuery(IRuntimeConnection conn, ClientContext context, IMacroValueProvider parameters, QueryRuntime qRuntime, int maxRows)
        {

            //Have the connective provide authentication informaiton
            conn.AuthenticateContext(context);
            
            //Build the query
            SPSearch.KeywordQuery q = new SPSearch.KeywordQuery(context);
            q.QueryText = qRuntime.QueryText.GenerateString(parameters);
            q.EnableOrderingHitHighlightedProperty = false;
            q.EnableInterleaving = false;

            //Set the timeout
            q.Timeout = conn.MaxTimeout * 1000;
                     

            //Get the any refiner passed
            string refiner = parameters.GetValueRaw(REFINER_PARAMETER);
            if(!string.IsNullOrWhiteSpace(refiner))
            {
                q.RefinementFilters.Add(refiner);
            }

            //Limit rows
            q.RowLimit = maxRows;
            q.RowsPerPage = maxRows;

            //Add any selected properties
            if(qRuntime.Query.RequestColumns.Count > 0)
            {
                qRuntime.Query.RequestColumns.ForEach(s => q.SelectProperties.Add(s));
            }

            SPSearch.SearchExecutor searchExecutor = new SPSearch.SearchExecutor(context);
            ClientResult<SPSearch.ResultTableCollection> results = searchExecutor.ExecuteQuery(q);
            context.ExecuteQuery();

           
            if(results.Value.Count > 0)
            {
                return results.Value[0];


            }else
            {
                return null;
            }




        }


        /// <summary>
        /// Executes the same search, except only requests a single row, but instead returns all the refinement values
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <param name="qRuntime"></param>
        /// <returns></returns>
        private SPSearch.ResultTable _ExecuteRefinerQuery(IRuntimeConnection conn, ClientContext context, IMacroValueProvider parameters, QueryRuntime qRuntime, string refiner)
        {

            //Have the connective provide authentication informaiton
            conn.AuthenticateContext(context);

            SPSearch.KeywordQuery q = new SPSearch.KeywordQuery(context);
            q.QueryText = qRuntime.QueryText.GenerateString(parameters);
            q.EnableOrderingHitHighlightedProperty = false;
            q.EnableInterleaving = false;
            q.EnableSorting = false;
            q.TrimDuplicates = false;
            q.Refiners = refiner;
           

            //Refiner query only asks for a single row (because all we are interested in are the refiner values)
            q.RowLimit = 1;
            q.RowsPerPage = 1;


            SPSearch.SearchExecutor searchExecutor = new SPSearch.SearchExecutor(context);
            ClientResult<SPSearch.ResultTableCollection> results = searchExecutor.ExecuteQuery(q);
            context.ExecuteQuery();

            //Only if there is a second results value
            if (results.Value.Count > 1)
            {
                return results.Value[1];
            }
            else
            {
                return null;
            }
            

        }

        #endregion


        #region Service Object Creation

        public List<ServiceObject> CreateServiceObjects(ServiceInstance service, QueryDef query)
        {
            //Create service objects for both the query and the refiners
            List<ServiceObject> list = new List<ServiceObject>();
            list.Add(_CreateQueryServiceObjects(service, query));
            list.Add(_CreateRefinerServiceObjects(service, query));

            return list;
        }

        private ServiceObject _CreateQueryServiceObjects(ServiceInstance service, QueryDef query)
        {

            ServiceObject so = service.ServiceObjects.Create(query.QueryId);
            so.MetaData.DisplayName = query.DisplayName;
            so.MetaData.Description = query.Description;
            so.MetaData.ServiceProperties.Add(METADATA_REFINERSEARCH, "false");

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
            method.MetaData.DisplayName = "List";
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
            foreach (var p in query.Parameters)
            {
                //Only for non-calculated columns
                if (!p.IsCalculated)
                {
                    //Create the parameter
                    ServiceObjectHelper.CreateMethodParameter(so, method, p.Name.ToUpper(), p.DisplayName, p.Description, (SoType)p.SMOType, true);

                }

            }

            //Now add a parameter for the Refinement Value
            ServiceObjectHelper.CreateMethodParameter(so, method, REFINER_PARAMETER, "Refiner Value", "Value for any refiners to add to the query", SoType.Memo, false);
                        

            //Finally Return the SO
            return so;


        }

        private ServiceObject _CreateRefinerServiceObjects(ServiceInstance service, QueryDef query)
        {

            ServiceObject so = service.ServiceObjects.Create(query.QueryId + REFINER_SUFFIX);
            so.MetaData.DisplayName = query.DisplayName + " Refiners";
            so.MetaData.Description = "Query Refiners for " + query.DisplayName;
            so.MetaData.ServiceProperties.Add(METADATA_REFINERSEARCH, "true");
            
            //Columns get added as Properties
            ServiceObjectHelper.CreateProperty(so, PROP_REFINERNAME, "Refiner Name",  "Name of the Managed Property Refiner", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, PROP_TYPE, "Type", "Value Type", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, PROP_SCORE, "Score", "Search Score", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, PROP_HITCOUNT, "Hit Count", "Number of hits on total document count", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, PROP_MIN, "Min", "Min Value", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, PROP_MAX, "Max", "Max Value", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, PROP_MEAN, "Mean", "Average value", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, PROP_REFINEMENT_NAME, "Value Name", "Refinment Name", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, PROP_REFINEMENT_DISPLAY, "Value Display", "Refinement name is displayable form", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, PROP_REFINEMENT_TOKEN, "Token", "Refinement token (pass back to Search Object to filter)", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, PROP_COUNT, "Count", "Count of items for refiner", SoType.Number);
            

            //Then there is a single List method with all the parameters
            //////////////////////////////////////////////////////////////////////////
            //LIST Method
            Method method = so.Methods.Create("List");
            method.MetaData.DisplayName = "List Refiner Values";
            method.MetaData.Description = "Retrieves a list of the refiner values for the query " + query.DisplayName;
            method.Type = MethodType.List;


            //No input properties for list


            //All Properties are for the return
            ReturnProperties returnProps = new ReturnProperties();
            foreach (Property p in so.Properties)
            {
                returnProps.Add(p);
            }
            method.ReturnProperties = returnProps;


            //Now all the query parameters (Same as for the query, so the refiners will match)
            ServiceObjectHelper.CreateMethodParameter(so, method, REFINER_PARAMETER, "Refiner Field", "Search Managed Property to get the list of refiner values for", SoType.Text, true);

            //Add all the same parameters as the query
            foreach (var p in query.Parameters)
            {
                //Only for non-calculated columns
                if (!p.IsCalculated)
                {
                    //Create the parameter
                    ServiceObjectHelper.CreateMethodParameter(so, method, p.Name.ToUpper(), p.DisplayName, p.Description, (SoType)p.SMOType, true);

                }

            }


            return so;
        }
        

        #endregion
        



    }
}
