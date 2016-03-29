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
*   Namespace: NAB.K2.SharePointSearch
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;
using NAB.K2.SharePointSearch.Broker;
using System.Diagnostics;
using System.Threading;


namespace NAB.K2.SharePointSearch
{
    public class SharePointSearchBroker : ServiceAssemblyBase, IRuntimeConnection
    {

        #region Constants
        public const string PROP_QUERYSTORE = "QueryStore";
        public const string PROP_MAXRECORDS = "MaxRecords";
        public const string PROP_MAXTIMEOUT = "MaxTimeout";
        public const string PROP_ALLOWCACHE = "AllowCache";
        public const string PROP_SPURL = "SPUrl";

        public const string METADATA_QUERYID = "QUERYID";

        public const string DEFAULT_CONFIGFILE = "C:\\Program Files (x86)\\K2 blackpearl\\ServiceBroker\\NAB.K2.SharePointSearch.conf";
        public const int DEFAULT_MAXRECORDS = 100;
        public const int DEFAULT_MAXTIMEOUT = 15;
        public const bool DEFAULT_ALLOWCACHE = false;
        public const string DEFAULT_SPURL = "https://<your sp url>/<optional sub-site>/";

        private const string DIAGCOL_QUERY = "QueryID";
        private const string DIAGCOL_Name = "Name";
        private const string DIAGCOL_Desc = "Description";
        private const string DIAGCOL_LastInit = "LastInitDate";
        private const string DIAGCOL_Execs = "Executions";
        private const string DIAGCOL_TotalSecs = "TotalSeconds";
        private const string DIAGCOL_AvgSecs = "AverageRuntime";
        private const string DIAGCOL_MaxSecs = "MaxRuntime";
        private const string DIAGCOL_Excep = "Exceptions";
        private const string DIAGCOL_CHist = "CacheHits";
        private const string DIAGCOL_CMiss = "CacheMisses";
        private const string DIAGCOL_CSize = "GlobeCacheSize"; 
        #endregion


        /// <summary>
        /// ID of the Diagnostic Folder
        /// </summary>
        public const string DIAG_FOLDERID = "NAB.K2.DIAG-FOLDER";

        /// <summary>
        /// ID of the Diagnostic Query Service Object
        /// </summary>
        public const string DIAG_QUERIES = "NAB.K2.DAIG-QUERIES";
        
        /// <summary>
        /// ID of the Diagnostic Functions Service Object
        /// </summary>
        public const string DIAG_FUNCTIONS = "NAB.K2.DAIG-FUNCTIONS";


        /// <summary>
        /// Accessor for the Service configuration setting for the configuration file
        /// </summary>
        public string ConfigStore { get { return this.Service.ServiceConfiguration[PROP_QUERYSTORE].ToString(); } }

        #region IRuntimeConnection Members

        //Property Accessors - note, this are read-only at timetime and should not be written to from any runtime class
        public Guid ServiceId { get { return this.ServiceInstanceGuid; } set { throw new Exception("SP Search Broker: Not allowed to change ServiceId at runtime"); } }
        public string SharePointUrl { get { return this.Service.ServiceConfiguration[PROP_SPURL].ToString(); } set { throw new Exception("SP Search Broker: Not allowed to change SP URL at runtime"); } }
        public int MaxRows { get { return Convert.ToInt32(this.Service.ServiceConfiguration[PROP_MAXRECORDS]); } set { throw new Exception("SP Search Broker: Not allowed to change Max Rows at runtime"); } }
        public int MaxTimeout { get { return Convert.ToInt32(this.Service.ServiceConfiguration[PROP_MAXTIMEOUT]); } set { throw new Exception("SP Search Broker: Not allowed to change Max Timeout at runtime"); } }


        public bool AllowCaching { 
            get 
            {
                AuthenticationMode m = this.Service.ServiceConfiguration.ServiceAuthentication.AuthenticationMode;

                if (m == AuthenticationMode.ServiceAccount || m == AuthenticationMode.Static)
                {
                    return Convert.ToBoolean(this.Service.ServiceConfiguration[PROP_ALLOWCACHE]); 

                }else
                {
                    //Cannot perform ANY caching on a service connection that is based on user credentials
                    //Thus, caching is only enabled when set to use Service Account or Static Credentials
                    return false;
                }
                
                
            
            } 
            set { throw new Exception("SP Search Broker: Not allowed to change Caching at runtime"); } 
        }
        
        /// <summary>
        /// Assigns the authentication information from the service instance into the Client Context
        /// </summary>
        /// <param name="context"></param>
        public void AuthenticateContext(Microsoft.SharePoint.Client.ClientContext context)
        {

            AuthenticationMode m = this.Service.ServiceConfiguration.ServiceAuthentication.AuthenticationMode;
            switch (m)
            {
                case AuthenticationMode.ServiceAccount:
                case AuthenticationMode.Impersonate:
                    //Use default network credentials
                    context.Credentials = CredentialCache.DefaultNetworkCredentials;
                    break;

                case AuthenticationMode.SSO:
                case AuthenticationMode.Static:
                    //Static and SSO use the same mechanisms    
                    string username = this.Service.ServiceConfiguration.ServiceAuthentication.UserName;
                    string password = this.Service.ServiceConfiguration.ServiceAuthentication.Password;

                    context.Credentials = new NetworkCredential(username, password);
                    break;
                case AuthenticationMode.OAuth:
                    //Get OAuth Token                    
                    throw new Exception("SP Search Broker: Sorry OAuth is not supported yet");
                   
            }


        }


        /// <summary>
        /// Runtime version of this cache will use the RuntimeConfirugation object to store these values by Service Instance
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public QueryRuntime GetQueryRuntime(QueryDef query)
        {
            //Retrieve the query runtime from the RuntimeConfiguration object that caches them
            return RuntimeConfiguration.GetRuntimeConfig(this).GetQueryRuntime(query, this);
        }


        #endregion


        #region Serivce Configuration
        public ServiceConfiguration SerivceConfiguration { get; set; }


        public override string GetConfigSection()
        {
            try
            {
                if (this.Service.ServiceConfiguration[PROP_QUERYSTORE] == null)
                    this.Service.ServiceConfiguration.Add(PROP_QUERYSTORE, true, DEFAULT_CONFIGFILE);

                if (this.Service.ServiceConfiguration[PROP_MAXRECORDS] == null)
                    this.Service.ServiceConfiguration.Add(PROP_MAXRECORDS, true, DEFAULT_MAXRECORDS);

                if (this.Service.ServiceConfiguration[PROP_MAXTIMEOUT] == null)
                    this.Service.ServiceConfiguration.Add(PROP_MAXTIMEOUT, true, DEFAULT_MAXTIMEOUT);

                if (this.Service.ServiceConfiguration[PROP_ALLOWCACHE] == null)
                    this.Service.ServiceConfiguration.Add(PROP_ALLOWCACHE, true, DEFAULT_ALLOWCACHE);

                if (this.Service.ServiceConfiguration[PROP_SPURL] == null)
                    this.Service.ServiceConfiguration.Add(PROP_SPURL, true, DEFAULT_SPURL);



            }
            catch (Exception)
            {
                //Swallow exceptions here
            }


            return base.GetConfigSection();
        }

        public override void Extend()
        {



        } 
        #endregion


        #region Describe Schema


        public override string DescribeSchema()
        {

            //Clear any currently cached configuration information for this service id
            RuntimeConfiguration.ClearServiceCache(this.ServiceInstanceGuid);


            //Create type mapper
            TypeMappings map = new TypeMappings();
            map.Add(typeof(System.Int16), SoType.Number);
            map.Add(typeof(System.Int32), SoType.Number);
            map.Add(typeof(System.Int64), SoType.Number);
            map.Add(typeof(System.UInt16), SoType.Number);
            map.Add(typeof(System.UInt32), SoType.Number);
            map.Add(typeof(System.UInt64), SoType.Number);
            map.Add(typeof(System.Boolean), SoType.YesNo);
            map.Add(typeof(System.Char), SoType.Text);
            map.Add(typeof(System.DateTime), SoType.DateTime);
            map.Add(typeof(System.Decimal), SoType.Decimal);
            map.Add(typeof(System.Single), SoType.Decimal);
            map.Add(typeof(System.Double), SoType.Decimal);
            map.Add(typeof(System.Guid), SoType.Guid);
            map.Add(typeof(System.Byte), SoType.File);
            map.Add(typeof(System.SByte), SoType.File);
            map.Add(typeof(System.String), SoType.Text);

            //Include nullables
            map.Add(typeof(Nullable<System.Int16>), SoType.Number);
            map.Add(typeof(Nullable<System.Int32>), SoType.Number);
            map.Add(typeof(Nullable<System.Int64>), SoType.Number);
            map.Add(typeof(Nullable<System.UInt16>), SoType.Number);
            map.Add(typeof(Nullable<System.UInt32>), SoType.Number);
            map.Add(typeof(Nullable<System.UInt64>), SoType.Number);
            map.Add(typeof(Nullable<System.Boolean>), SoType.YesNo);
            map.Add(typeof(Nullable<System.Char>), SoType.Text);
            map.Add(typeof(Nullable<System.DateTime>), SoType.DateTime);
            map.Add(typeof(Nullable<System.Decimal>), SoType.Decimal);
            map.Add(typeof(Nullable<System.Single>), SoType.Decimal);
            map.Add(typeof(Nullable<System.Double>), SoType.Decimal);
            map.Add(typeof(Nullable<System.Guid>), SoType.Guid);
            map.Add(typeof(Nullable<System.Byte>), SoType.File);
            map.Add(typeof(Nullable<System.SByte>), SoType.File);

            //Add mapper to service configuration
            this.Service.ServiceConfiguration.Add("Type Mappings", map);
            
            //Gets the current runtime configuration
            RuntimeConfiguration config = RuntimeConfiguration.GetRuntimeConfig(this);
                        
            //Load the standard accessor methods
            _LoadOperationalSchema();
            
            //Load the configuration from the configuration store
            _LoadConfigSchema(config);
            
            //Setup Service Instance
            ServicePackage.IsSuccessful = true;


            return base.DescribeSchema();
        }

        private void _LoadConfigSchema(RuntimeConfiguration config)
        {

            ConfigurationStore store = config.ConfigStore;

            //Currently only support single level folder structure
            foreach(StoreFolder f in store.Folders)
            {
                ServiceFolder folder = this.Service.ServiceFolders.Create(f.FolderId);
                folder.MetaData.DisplayName = f.DisplayName;
                folder.MetaData.Description = f.Description;

               
                //Get all the queries by folder
                var queries = (from q in store.Queries where q.FolderId == f.FolderId select q).ToList();
                foreach(var q in queries)
                {
                    //Load all the service objects for the query
                    var soList = _LoadQuery(q);
                    foreach(var so in soList)
                    {
                        //Force add metadata for the Query Id
                        //This is so we can make sure to always find the proper query for a service object
                        so.MetaData.ServiceProperties.Add(METADATA_QUERYID, q.QueryId);

                        //Add to folder
                        folder.Add(so);
                        
                        //Activate
                        so.Active = true;
                    }

                }

            }

        }

        private List<ServiceObject> _LoadQuery(QueryDef query)
        {
            //Get the Engine for this query
            IQueryEngine engine = EngineController.GetEngine(query.Engine);

            //Have the engine build the service objects
            return engine.CreateServiceObjects(this.Service, query);

        }


        private void _LoadOperationalSchema()
        {

            ServiceFolder folder = this.Service.ServiceFolders.Create(DIAG_FOLDERID);
            folder.MetaData.DisplayName = "System Diagnostics";
            folder.MetaData.Description = "Provides diagnotics and operational methods for this instance";

            ServiceObject so = _LoadDiag();

            //Add to folder
            folder.Add(so);

            //Activate
            so.Active = true;
            
        }

        private ServiceObject _LoadDiag()
        {

            ServiceObject so = this.Service.ServiceObjects.Create(DIAG_QUERIES);
            so.MetaData.DisplayName = "Queries";
            so.MetaData.Description = "Lists query details and diagnostic information";

            //Columns get added as Properties
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_QUERY, "Query Id", "Internal Id of the query", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_Name, "Name", "Display Name", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_Desc, "Description", "Query Description", SoType.Text);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_LastInit, "Last Init date", "Last Date Initialized", SoType.DateTime);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_Execs, "Executions", "Number of time run since last initialized", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_TotalSecs, "Total Seconds", "Total Number of Seconds Running", SoType.Decimal);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_AvgSecs, "Average Runtime", "Average Time Per", SoType.Decimal);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_MaxSecs, "Maximum Runtime", "Max Time Per", SoType.Decimal);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_Excep, "Exceptions", "Number of Exceptions", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_CHist, "Cache Hits", "Number of Cache Hits", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_CMiss, "Cache Misses", "Number of Cache Misses", SoType.Number);
            ServiceObjectHelper.CreateProperty(so, DIAGCOL_CSize, "Global Cache Size", "Global Cache Size (bytes)", SoType.Number);


            //Then there is a single List method with all the parameters
            //////////////////////////////////////////////////////////////////////////
            //LIST Method
            Method method = so.Methods.Create("List");
            method.MetaData.DisplayName = "List Diagnotics";
            method.MetaData.Description = "Lists currently loaded queries and diagnostic information";
            method.Type = MethodType.List;


            //No input properties for list


            //All Properties are for the return
            ReturnProperties returnProps = new ReturnProperties();
            foreach (Property p in so.Properties)
            {
                returnProps.Add(p);
            }
            method.ReturnProperties = returnProps;


            //No parameters
            

            return so;

        }

        #endregion


        #region Execution

        public override void Execute()
        {

            //Get runtime environment
            RuntimeConfiguration config = RuntimeConfiguration.GetRuntimeConfig(this);
            
            ServiceObject serviceObject = this.Service.ServiceObjects[0];
            
            Method method = serviceObject.Methods[0];
            
            //Check for service office
            if(serviceObject.Name == DIAG_QUERIES)
            {
                _ExecuteDiagnotic(config, serviceObject, method);
                return;
            }

            //populate the return properties collection
            Property[] returnProps = new Property[method.ReturnProperties.Count];
            for (int i = 0; i < method.ReturnProperties.Count; i++)
            {
                returnProps[i] = serviceObject.Properties[method.ReturnProperties[i]];
            }

            //Get the query id
            string queryId = (string)serviceObject.MetaData.ServiceProperties[METADATA_QUERYID] ?? serviceObject.Name;
            
            //Get the query
            QueryDef query = config.GetQuery(queryId);
            QueryRuntime qRuntime = this.GetQueryRuntime(query);

            //populate the inputs collection
            RuntimePropertyBox box = new RuntimePropertyBox();
            for (int i = 0; i < method.InputProperties.Count; i++)
            {
                box.AddParameter(serviceObject.Properties[method.InputProperties[i]]);
            }

            for (int i = 0; i < method.MethodParameters.Count; i++)
            {
                box.AddParameter(method.MethodParameters[i]);
            }
            
            //Create the macro provider with all parameters and runtimes config
            RuntimeMacroProvider macros = new RuntimeMacroProvider(qRuntime, box);
                        
            //Get the Engine
            IQueryEngine engine = EngineController.GetEngine(query.Engine);

            //Create Cancelation Token to ensure we effectively perform Timeout operations and not leave dangling tasks
            var cancelTokenSource = new CancellationTokenSource();

            //Using block to dispose the Task
            using(Task<bool> t = new Task<bool>(() => engine.ExecuteToK2(this, macros, qRuntime, this, serviceObject, returnProps, cancelTokenSource.Token)))
            {
                
                //Stopwatch to record time
                Stopwatch s = new Stopwatch();

                //Exceute within try block to ensure we catch Exceptions
                try
                {
                    //Start the stopwatch
                    s.Start();

                    //Start the task
                    t.Start();

                    //Schedules a cancelation
                    cancelTokenSource.CancelAfter(query.QueryTimeoutSeconds * 1000);

                    //Wait for the task to complete or until timeout
                    t.Wait(cancelTokenSource.Token);

                }
                catch (OperationCanceledException)
                {
                    //This was canceled due to timeout
                    ServicePackage.IsSuccessful = false;
                    qRuntime.PerfAddException("Timeout");
                    throw new QueryTimeoutException();

                }
                catch (Exception e)
                {
                    //Get the full exception message from all inner exceptions
                    string msg = e.GetFullMessage();

                    //Log
                    qRuntime.PerfAddException(msg);

                    //Re-throw
                    throw new Exception(msg, e);

                }
                finally
                {
                    //Dispose of the cancelation token
                    cancelTokenSource.Dispose();

                    //Ensure timer is stopped
                    if (s.IsRunning)
                    {
                        s.Stop();
                    }

                }

                //Check for compelte
                if (t.IsCompleted)
                {
                    //record the performance
                    qRuntime.PerfAddSucccess(s.Elapsed.TotalSeconds);

                    //Indicate success
                    ServicePackage.IsSuccessful = true;

                }
            
               
            }  //End of Task using block          


        }


        private void _ExecuteDiagnotic(RuntimeConfiguration config, ServiceObject serviceObject, Method method)
        {

            //Get all the current runtimes
            List<QueryRuntime> qrList = config.GetAllRuntimes();


            //Initialize Results Table
            serviceObject.Properties.InitResultTable();

            long itemCount = System.Runtime.Caching.MemoryCache.Default.GetCount();

            //Loop through the items in the collection
            foreach (var qr in qrList)
            {
                             
                serviceObject.Properties[DIAGCOL_QUERY].Value = qr.QueryId;
                serviceObject.Properties[DIAGCOL_Name].Value = qr.Query.DisplayName;
                serviceObject.Properties[DIAGCOL_Desc].Value = qr.Query.Description;
                serviceObject.Properties[DIAGCOL_LastInit].Value = qr.PerfInitDate;
                serviceObject.Properties[DIAGCOL_Execs].Value = qr.PerfTotalExecute;
                serviceObject.Properties[DIAGCOL_TotalSecs].Value = qr.PerfTotalTime;
                serviceObject.Properties[DIAGCOL_AvgSecs].Value = (qr.PerfTotalExecute > 0 ? (qr.PerfTotalTime / (double)qr.PerfTotalExecute) : 0);
                serviceObject.Properties[DIAGCOL_MaxSecs].Value = qr.PerfMaxTime;
                serviceObject.Properties[DIAGCOL_Excep].Value = qr.PerfExceptions;
                serviceObject.Properties[DIAGCOL_CHist].Value = qr.PerfCacheHits;
                serviceObject.Properties[DIAGCOL_CMiss].Value = qr.PerfCacheMisses;
                serviceObject.Properties[DIAGCOL_CSize].Value = itemCount;


                serviceObject.Properties.BindPropertiesToResultTable();
            } 


            
        }


        #endregion

    }
}
