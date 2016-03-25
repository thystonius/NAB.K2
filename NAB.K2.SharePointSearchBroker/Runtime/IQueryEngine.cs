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
using System.Data;
using System.Threading.Tasks;

using NAB.K2.SharePointSearch.Configuration;


using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System.Threading;

namespace NAB.K2.SharePointSearch.Runtime
{

    /// <summary>
    /// Interface that all engines must implement
    /// Engines should be considered stateless
    /// All methods of engines MUST be thread-safe
    /// </summary>
    public interface IQueryEngine
    {
        
        /// <summary>
        /// Retrieves a list of the actual Return Columns from a query
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        List<ReturnColumn> RetrieveOutputColumns(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime query);


        /// <summary>
        /// Used for testing queries only
        /// Execute the query to the provided service object
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        bool ExecuteToK2(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime query, ServiceAssemblyBase broker, ServiceObject serviceObject, Property[] returnProperties, CancellationToken cancelToken);


        /// <summary>
        /// Used for testing queries only
        /// Probably should be replaced with just a simple array, but this was easier and since it is only for testing, it probably isn't a big deal
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        DataTable ExecuteToDataTable(IRuntimeConnection conn, IMacroValueProvider parameters, QueryRuntime query);


        List<ServiceObject> CreateServiceObjects(ServiceInstance service, QueryDef query);

    }
}
