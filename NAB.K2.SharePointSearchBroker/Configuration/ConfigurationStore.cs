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
using System.Threading.Tasks;

namespace NAB.K2.SharePointSearch.Configuration
{

    /// <summary>
    /// Contains the MetaData for all the queries that the broker is capable of executing
    /// This object is the non-optomized data store for the configuration
    /// ALL properties should be serialized and stored, any other indexing and optomization should be done in an extension class
    /// </summary>
    public class ConfigurationStore
    {

        public ConfigurationStore() { }

        public ConfigurationStore(bool useDefaults)
        {
            if (useDefaults)
            {
                StoreName = "New SharePoint Queries";
                StoreVersion = "1.0.0.0";
                StoreBuild = 0;

            }

        }

        /// <summary>
        /// Arbitrary Store Name to help identify this store
        /// </summary>
        public string StoreName { get; set; }


        /// <summary>
        /// Arbitrary Version string to help in the event that the store is stored in a version control system
        /// </summary>
        public string StoreVersion { get; set; }

        /// <summary>
        /// Number that is automatically incramented during each save operation.  In this way it can be used an automated versioning.
        /// </summary>
        public int StoreBuild { get; set; }

        /// <summary>
        /// All the queries
        /// </summary>
        public List<QueryDef> Queries = new List<QueryDef>();
        
        
        /// <summary>
        /// Folders 
        /// </summary>
        public List<StoreFolder> Folders = new List<StoreFolder>();
        

    }
}
