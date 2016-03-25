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
using System.Runtime.Caching;


namespace NAB.K2.SharePointSearch.Runtime
{
    /// <summary>
    /// Class that handle all the cache requests
    /// </summary>
    internal class RuntimeCache
    {

        /// <summary>
        /// Concurrency lock object
        /// </summary>
        private static readonly object _cacheLock = new object();

        


        public static object CacheRetrieve(string key)
        {

            return MemoryCache.Default.Get(key);

        }

        public static bool CacheContains(string key)
        {

            return MemoryCache.Default.Contains(key);

        }

        public static void CacheStore(string key, object value, int seconds)
        {

            if(key == null || value == null)
            {
                return;
            }
            
            lock(_cacheLock)
            {
                if(MemoryCache.Default.Contains(key))
                {
                    //This has been added to cache before we got here.
                    return;
                }

                //Create cache policy with absolute expiration and a callback to dispose of objects
                CacheItemPolicy p = new CacheItemPolicy();
                p.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(seconds));
                p.RemovedCallback = new CacheEntryRemovedCallback(CacheRemovedCallback);
                
                //Add to cache
                MemoryCache.Default.Set(key, value, p);

            }
                        
        }

        /// <summary>
        /// Callback to dispose of any disposable objects that are removed from cache
        /// </summary>
        /// <param name="arguments"></param>
        public static void CacheRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            //Attemp to dispose anything that was in here
            var d = arguments.CacheItem.Value as IDisposable;
            if (d != null)
            {
                d.Dispose();
            }



        }

    }
}
