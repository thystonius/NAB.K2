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
*   Namespace: NAB.K2.SharePointSearch.Broker
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;


namespace NAB.K2.SharePointSearch.Broker
{
    /// <summary>
    /// This class is used EXLCUSIVELY by the Broker to cache runtime configuration
    /// This class should not be called or referenced by anyone other than the broker
    /// </summary>
    public class RuntimeConfiguration 
    {
        
        private static Dictionary<Guid, RuntimeConfiguration> _runtimeConfig = new Dictionary<Guid,RuntimeConfiguration>();
        private static object _runtimeLock = new object();
        
        //Gets a runtime configuration from the cache
        public static RuntimeConfiguration GetRuntimeConfig(SharePointSearchBroker svc)
        {

            if(!_runtimeConfig.ContainsKey(svc.Service.Guid))
            {

                //Be sure to lock the dictionary to ensure proper threading
                lock(_runtimeLock)
                {
                    if (!_runtimeConfig.ContainsKey(svc.Service.Guid))
                    {
                        //Config Store
                        ConfigurationStore store;
                        
                        //Simple URL check for prefix
                        if(svc.ConfigStore.StartsWith("https://") || svc.ConfigStore.StartsWith("http://"))
                        {
                            //Load from url
                            store = ConfigurationLoader.LoadStoreFromUrl(svc.ConfigStore);
                        }
                        else
                        {
                            //Load the configuration file
                            store = ConfigurationLoader.LoadStoreFromFile(svc.ConfigStore);
                        }


                        
                        //Create a new runtime config object
                        RuntimeConfiguration r = new RuntimeConfiguration(store);

                        _runtimeConfig.Add(svc.Service.Guid, r);
                    }
                }
            }

            return _runtimeConfig[svc.Service.Guid];

        }

        /// <summary>
        /// Removes the runtime instance from the dictionary
        /// </summary>
        /// <param name="serviceId"></param>
        public static void ClearServiceCache(Guid serviceId)
        {
            if(_runtimeConfig.ContainsKey(serviceId))
            {
                _runtimeConfig.Remove(serviceId);
            }

            return;
        }


        //Private members that are cached as part of the runtime configuration
        private ConfigurationStore _store;
        private DateTime _loadDate;


        private Dictionary<string, QueryRuntime> _queryRuntimes = new Dictionary<string, QueryRuntime>();
        private object _queryLock = new object();

        //cached dictionaries used to enhance runtime performance
        private Dictionary<string, QueryDef> _queries;
        
        public RuntimeConfiguration(ConfigurationStore store)
        {
            _store = store;
            _loadDate = DateTime.Now;

            _queries = new Dictionary<string, QueryDef>();
            foreach(QueryDef q in _store.Queries)
            {
                _queries.Add(q.QueryId, q);
            }


        }

        /// <summary>
        /// Configuration
        /// </summary>
        public ConfigurationStore ConfigStore { get { return _store; } }


        public QueryDef GetQuery(string name)
        {
            try
            {
                return _queries[name];           

            }catch(Exception e)
            {
                throw new Exception(string.Format("Query Id not found {0}", name), e);
            }
            

        }

        
        /// <summary>
        /// Obtains and caches the query runtimes so any string manipulation and buffering of queries can be minimized during runtime
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public QueryRuntime GetQueryRuntime(QueryDef query, IRuntimeConnection conn)
        {

            //Next we lookup this connection
            if (!_queryRuntimes.ContainsKey(query.QueryId))
            {

                //Build the queryRuntime (outside of lock to minimize time in lock)
                QueryRuntime qr = new QueryRuntime(query, conn);

                //Lock Dictionary
                lock (_queryLock)
                {
                    //Double check
                    if (!_queryRuntimes.ContainsKey(query.QueryId))
                    {
                        //Add to dic
                        _queryRuntimes.Add(query.QueryId, qr);

                    }
                }
            }


            //Return
            return _queryRuntimes[query.QueryId];

        }


        /// <summary>
        /// Retrieves all the currently loaded query runtimes
        /// </summary>
        /// <returns></returns>
        public List<QueryRuntime> GetAllRuntimes()
        {

            return _queryRuntimes.Values.ToList();

        }


    }
}
