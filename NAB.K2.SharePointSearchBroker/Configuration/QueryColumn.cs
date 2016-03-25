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
    /// Class defined a return column for a query
    /// </summary>
    public class QueryColumn : PropertyBase
    {

        /// <summary>
        /// Name of the column from the data source
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// Type of the column at the data source
        /// </summary>
        public string ColumnType { get; set; }
        
        /// <summary>
        /// Indicates if this column should be returned as part of the data set into K2
        /// </summary>
        public bool Include { get; set; }



    }
}
