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
*   Namespace: NAB.K2.SharePointSearch.Configuration
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace NAB.K2.SharePointSearch.Configuration
{
    public class ConfigurationLoader
    {

        /// <summary>
        /// Load the configuration store from a json file via standard file read
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <returns></returns>
        public static ConfigurationStore LoadStoreFromFile(string filename)
        {

            string json = File.ReadAllText(filename);
            ConfigurationStore s = LoadStoreFromString(json);
            
            return s;
        }

        /// <summary>
        /// Load the file from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ConfigurationStore LoadStoreFromUrl(string url)
        {

            string json;

            using(var client = new WebClient())
            {
                //Use the default credentials
                client.UseDefaultCredentials = true;

                try
                {
                    json = client.DownloadString(url);
                }catch(Exception e)
                {
                    throw new Exception(string.Format("Failed to download configuration file with error: {0}", e.Message), e);
                }
                
            }
            
            ConfigurationStore s = LoadStoreFromString(json);

            return s;
        }


        public static ConfigurationStore LoadStoreFromString(string json)
        {

            ConfigurationStore s = JsonConvert.DeserializeObject<ConfigurationStore>(json);

            return s;
        }

        public static bool SaveStoreFromFile(ConfigurationStore store, string filename)
        {
            //Incrament the store build
            store.StoreBuild++;

            string json = JsonConvert.SerializeObject(store);
            File.WriteAllText(filename, json);

            return true;

        }


    }
}
