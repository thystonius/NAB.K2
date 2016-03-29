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
    /// Configuration Settings and Definition of a Query
    /// Should ONLY contain public properties as this is the class that gets serialized as part of the configuration store
    /// NO private or instance methods, use an extension class for those
    /// NO runtime only or Processed values as these should use a runtime optomized class for that
    /// </summary>
    public class QueryDef
    {

        /// <summary>
        /// Immutable Unique Identity for the Query
        /// Arbitrary string identifier
        /// </summary>
        public string QueryId { get; set; }

        /// <summary>
        /// Display Name of the Query
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Metadata desription
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// ID of the Folder for this Query
        /// </summary>
        public string FolderId { get; set; }
        
        /// <summary>
        /// Inidicates the execution class that should handle this query
        /// </summary>
        public int Engine { get; set; }

        /// <summary>
        /// Maximum
        /// </summary>
        public int MaxRecords { get; set; }

        /// <summary>
        /// Number of seconds that the results of this query can be cached
        /// Zero or less indicates no caching
        /// It is highly advisable to use this wisely.  For example, caching for 30 seconds may be of little to no use.  Neither will caching for a week.
        /// </summary>
        public int CacheSeconds { get; set; }


        /// <summary>
        /// This is the portion of the path for the Site
        /// SharePoint Client requires opening a site separate and distinct from the URL to the list
        /// So this path will only contain the site as a relative root from the one specified in service instance
        /// </summary>
        public string RelativeSiteUrl { get; set; }


        /// <summary>
        /// The relative URL that this query should be executed on.
        /// SHOULD NOT store full url, instead put the base url in the ServiceBroker instance and then this becomes a relative URL from that.
        /// This keeps the full URLs out of configuration files and in the Service Broker instance 
        /// This allows configuration files can be shared between production and test with potentially no modification
        /// </summary>
        public string RelativeUrl { get; set; }


        /// <summary>
        /// Number of seconds before the query times out
        /// </summary>
        public int QueryTimeoutSeconds { get; set; }


        /// <summary>
        /// Indicates if Search should use FQL or KQL
        /// </summary>
        public bool UseFQL { get; set; }

        /// <summary>
        /// Query Command text as defined by the engine
        /// </summary>
        public string QueryText { get; set; }


        /// <summary>
        /// List of extra requested columns that can be used by the Engine
        /// How these are used is engine specific
        /// </summary>
        public List<string> RequestColumns = new List<string>();
        
        /// <summary>
        /// Input parameters
        /// Note, these are query specific parameters, additional built-in parameters will also be added (such as page size)
        /// </summary>
        public List<QueryParameter> Parameters = new List<QueryParameter>();


        /// <summary>
        /// Return Columns
        /// </summary>
        public List<QueryColumn> Columns = new List<QueryColumn>();


        

    }
}
