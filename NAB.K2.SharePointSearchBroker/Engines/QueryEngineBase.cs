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
using System.Text;
using System.Threading.Tasks;

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;

namespace NAB.K2.SharePointSearch.Engines
{
    /// <summary>
    /// Base class that Query Engines can use to refactor common methods
    /// Engines are not required to implement from this base class, only implement IQueryEngine
    /// 
    /// You may ask why functions such as ProcessColumnOutput are not centralized so they are processed the same way
    /// </summary>
    public class QueryEngineBase
    {
        
        /// <summary>
        /// Performs any Post-Process operations on a column based on the parameters provided by the configuration columns
        /// </summary>
        /// <param name="column">Configuration settings</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public object ProcessColumnOutput(QueryColumn column, IMacroValueProvider macros, object value)
        {
  


            return value;

        }
        

    }

}
