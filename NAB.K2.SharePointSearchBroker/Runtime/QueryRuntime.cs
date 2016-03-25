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
*   Namespace: NAB.K2.SharePointSearch.Runtime
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NAB.K2.SharePointSearch.Configuration;

namespace NAB.K2.SharePointSearch.Runtime
{

    /// <summary>
    /// Class that supliments the QueryDef with properties that are best calculated once during runtime and then reused for entire application context
    /// These values should never be stored and are only applicable to the executing engine during runtime
    /// This is intended to optomize for run-time at the cost of continually rebuilding during design
    /// </summary>
    public class QueryRuntime
    {
        //Primary Query Definition
        private readonly QueryDef _query;
        
        //Mergable Strings
        private MergableString _siteUrl;
        private MergableString _fullUrl;
        private MergableString _queryText;

        //Prepared Properties
        private int _actualTimeoutSeconds = 0;
        
        /// <summary>
        /// Caching is disabled by default and is only enabled if all conditions are met
        /// </summary>
        private bool _cachingEnabled = false;
        private string _cachePrefix = null;


        //Performance Metrics
        private readonly DateTime _perfInitDate;
        private int _perfTotalExecute = 0;
        private double _perfTotalTime = 0;
        private double _perfMaxTime = 0;
        private int _perfExceptions = 0;
        private int _perfCacheHits = 0;
        private int _perfCacheMisses = 0;

       
        //Columns and Parameters
        private Dictionary<string, QueryParameter> _parameters;
        private Dictionary<string, QueryColumn> _returnColumns;

        public DateTime PerfInitDate { get { return _perfInitDate; } }
        public int PerfTotalExecute { get { return _perfTotalExecute; } }
        public double PerfTotalTime { get { return _perfTotalTime; } }
        public double PerfMaxTime { get { return _perfMaxTime; } }
        public int PerfExceptions { get { return _perfExceptions; } }
        public int PerfCacheHits { get { return _perfCacheHits; } }
        public int PerfCacheMisses { get { return _perfCacheMisses; } }
                
        /// <summary>
        /// Maps Internal Names (ones provided to K2) the the source column name 
        /// </summary>
        private Dictionary<string, string> _returnLookup;

        //Public Read-Only Accessors
        public MergableString SiteUrl { get { return _siteUrl; } }
        public MergableString FullUrl { get { return _fullUrl; } }
        public MergableString QueryText { get { return _queryText; } }

        public int QueryTimeout { get { return _actualTimeoutSeconds; } }
        public bool CachingEnabled { get { return _cachingEnabled;  } }

        public string QueryId { get { return _query.QueryId; } }
        public QueryDef Query { get { return _query; } }


        /// <summary>
        /// Pre-builds an values that would speed up the execution of this query during runtime
        /// Act's like a pre-compile, expecially for any string manipulation that should not take place during each execution
        /// </summary>
        /// <param name="query"></param>
        public QueryRuntime(QueryDef query, IRuntimeConnection conn)
        {

            //Set the init date - Remember all are lazy loaded so each can be different
            _perfInitDate = DateTime.Now;
            
            //Remember the query
            _query = query;
            
            if(_query.QueryTimeoutSeconds <= 0 || _query.QueryTimeoutSeconds > conn.MaxTimeout)
            {
                _actualTimeoutSeconds = conn.MaxTimeout;
            }

            //Build the URL
            UrlBuilder b = new UrlBuilder(conn.SharePointUrl);
            b.AppendPath(query.RelativeSiteUrl);
            
            //Stop here and add this as the Site Url
            _siteUrl = new MergableString(b.ToString());

            //Add relative list url
            b.AppendPath(query.RelativeUrl);            
            _fullUrl = new MergableString(b.ToString());
                        
            //Main Query text
            _queryText = new MergableString(_query.QueryText);


            //Convert to dictionary for easy lookup access
            //NOTE - WE ARE FORCING ToUpper for all parameter names, this is done only once on load so we don't have to do it everywhere during runtime
            _parameters = (from c in query.Parameters select new { c.Name, c }).ToDictionary(t => t.Name.ToUpper(), t=> t.c);

            _returnColumns = (from c in query.Columns where c.Include == true select c).ToDictionary(t => t.SourceColumn, t => t, StringComparer.InvariantCultureIgnoreCase);
            _returnLookup = (from c in query.Columns where c.Include == true select c).ToDictionary(t => t.Name, t => t.SourceColumn, StringComparer.InvariantCultureIgnoreCase);

            //Check for caching
            if (conn.AllowCaching)
            {
                if (query.CacheSeconds > 0)
                {
                    //So far so good
                    _cachingEnabled = true;

                    //Check for any calculated parameters
                    if(query.Parameters.Count > 0)
                    {
                        if(query.Parameters.Any(p => p.IsCalculated == true))
                        {
                            //We cannot handle caching when there are Calculated Parameter
                            //Mostly because this would probably not be used wisely
                            //For example, caching return results based on a calculated date is dumb
                            _cachingEnabled = false;
                        }
                    }

                    if(_cachingEnabled)
                    {
                        //Set the query prefix
                        _cachePrefix = conn.ServiceId.ToString() + _query.QueryId + "-";
                    }
                    
                }
            }
           
        }

        #region Parameters and Columns

        public QueryParameter GetParameter(string name)
        {
            if(_parameters.ContainsKey(name))
            {
                return _parameters[name];
            }
            else
            {
                return null;
            }


        }

        public QueryColumn GetColumn(string sourceName)
        {
            if(_returnColumns.ContainsKey(sourceName))
            {
                return _returnColumns[sourceName];
            }
            else
            {
                return null;
            }
        }

        public string GetReturnName(string name)
        {
            if (_returnLookup.ContainsKey(name))
            {
                return _returnLookup[name];
            }
            else
            {
                return null;
            }
        }
        
        #endregion


        #region Caching

        /*
            How Caching Works for this Broker
         *  The design of caching for this broker is to minimze the need to pre-calculate any values in parameters
         *  Caching sequence:
         *      CacheGenerateId = Generates a string id (based on a Hash Value passed by the query engine)
         *      CacheRetrieve = Engine Calls the Retrieve method to check for cached item
         *      <engine> = if retrieved null then execute query
         *      CacheStore = Called by engine to store new value into cache
         * 
        
         *  Note the query runtime doesn't know anything about the specific parameters being used (nor should it, it is only the place to pre-compile values that don't change such as pre-merged query text)
         *  So the only place that knows about the values that do change is the Parameter Box (remember some can be calculated)
         *  Thus, we have to wait until all parameters are calculated to be able to hash them and determine if we have a cache hit
         *  The problem is that the query runtime doesn't know the specifics of the engine (what is a significant change and what is not)
         *  So this function only provide access methods to the cache and lets the engine decide how it want to hash the values to determine uniqueness for it's value
         *  This means that more thought need to be placed into the engine to best determine caching
         *  The reason this was chosen as an architecture is because there is no "one size fits all" for caching and special care should be used when building engines to ensure that caching is optomized for
         *  that particual source.  Note we cache slightly differently between a CAML query and a Search Query
         *  Naturally the QRuntime does add a prefix to ensure that it is for this service instance and query, but leaves the rest up to the engine
         *  
         *  This entire caching could be refactored into another class set, but before doing so it should be evaluated if that make the code perform any better (emphasis should be on performance here)
        */

        /// <summary>
        /// Retrieves the item from the Cache, if not found or not enabled, then returns null
        /// </summary>
        /// <param name="parameterHash">Provided by the parameter container to indicate unique values for parameters</param>
        /// <returns></returns>
        public string CacheGenerateId(UInt64 parameterHash)
        {
            
            if(!this.CachingEnabled)
            {
                return null;
            }

            //Hashes cannot be zero
            if(parameterHash == 0)
            {
                return null;
            }

            //Build the id
            return _cachePrefix + parameterHash.ToString("X");
        }


        /// <summary>
        /// Retrieves the value from the Cache for the given key
        /// </summary>
        /// <param name="key">Key provided by CacheGenerateId</param>
        /// <returns></returns>
        public object CacheRetrieve(string key)
        {
            //Short circuit for not enabled
            if(key == null || !this.CachingEnabled)
            {
                return null;
            }


            if(RuntimeCache.CacheContains(key))
            {
                //Incrament performance counter for a cache hit;
                _perfCacheHits++;
                return RuntimeCache.CacheRetrieve(key);
            }
            else
            {
                _perfCacheMisses++;
                return null;
            }

        }

        /// <summary>
        /// Stores the results into the cache
        /// </summary>
        /// <param name="key">Key provided by CacheGenerateId</param>
        /// <param name="value">Value to store</param>
        public void CacheStore(string key, object value)
        {
            //Short circuit for not enabled
            if (key == null || !this.CachingEnabled)
            {
                return;
            }

            //Set with expiration based on query cache seconds
            RuntimeCache.CacheStore(key, value, _query.CacheSeconds);

        }

        #endregion


        #region Performance Measurement

        public void PerfAddSucccess(double timetaken)
        {
            //Record total
            _perfTotalExecute++;
            _perfTotalTime += timetaken;
            
            //Reset max
            if(timetaken > _perfMaxTime)
            {
                _perfMaxTime = timetaken;
            }

        }

        public void PerfAddException(string message)
        {
            _perfExceptions ++;
        }

        #endregion


    }
}
