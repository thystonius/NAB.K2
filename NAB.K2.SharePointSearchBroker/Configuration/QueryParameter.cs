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
    /// Query Parameter Definition
    /// </summary>
    public class QueryParameter : PropertyBase
    {

        /// <summary>
        /// Indicates if this parameter must be provided.
        /// Exception will be thrown if left empty / null.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Default value if no value was provided
        /// Only applicable if the parameter is not required
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Indicates if this parameter is calculated at runtime (not passed in)
        /// </summary>
        public bool IsCalculated { get; set; }
        
        /// <summary>
        /// Provides the calculation method to calculate the parameter
        /// </summary>
        public string Calculation { get; set; }


        /// <summary>
        /// Value used during design time to test
        /// So you don't have to keep entering values to test your query
        /// </summary>
        public string DesignTimeValue { get; set; }


    }
}
