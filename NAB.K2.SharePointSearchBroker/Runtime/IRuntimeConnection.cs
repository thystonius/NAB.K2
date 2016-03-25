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

using Microsoft.SharePoint.Client;

using NAB.K2.SharePointSearch.Configuration;

namespace NAB.K2.SharePointSearch.Runtime
{
    public interface IRuntimeConnection
    {


        /// <summary>
        /// Unique service id to identify this connection
        /// </summary>
        Guid ServiceId { get; set; }

        /// <summary>
        /// Base url that all query URLs should be appended to
        /// This keeps all actual URLs out of the configuration file
        /// </summary>
        string SharePointUrl { get; set; }


        /// <summary>
        /// Maximum Number of rows that the engine should ever return
        /// </summary>
        int MaxRows { get; set; }

        /// <summary>
        /// Maximum number of seconds that ANY query on this connection can run - global timeout limit for all queries
        /// </summary>
        int MaxTimeout { get; set; }

        /// <summary>
        /// Indicates if caching is enabled for this connection
        /// Cacheing is ONLY allowed when authentication is set to Static or Service Credentials
        /// </summary>
        bool AllowCaching { get; set; }

        
        /// <summary>
        /// Method called by a query engine that asks the Connect to populate authenticate information into the client context
        /// This keeps the Query Engine out of the authenticate business
        /// Also keeps ALL authenticate information out of confiruation and puts it in the hads of the caller
        /// </summary>
        void AuthenticateContext(ClientContext context);


        /// <summary>
        /// Gets the runtime "processed" query for runtime execution.  This version has significantly more caching and specifically optomized for heavy runtime usage
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        QueryRuntime GetQueryRuntime(QueryDef query);


    }
}
