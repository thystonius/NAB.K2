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

namespace NAB.K2.SharePointSearch.Runtime
{

    /// <summary>
    /// Used by MergableString class to define a method to retrieve value values
    /// NOT to be confused with parameter provides which only supply values
    /// See QueryRuntime for example of an implementation
    /// </summary>
    public interface IMacroValueProvider
    {
        /// <summary>
        /// Gets the value of a macro in context of the execution runtime
        /// Should be implemented to use any calcuation methods necessary
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetMacroValue(string name);


        /// <summary>
        /// Returns a raw value without any calculation
        /// </summary>
        /// <param name="name">Name or the parameter or macro</param>
        /// <returns>Raw untouched value</returns>
        string GetValueRaw(string name);
        

    }
}
