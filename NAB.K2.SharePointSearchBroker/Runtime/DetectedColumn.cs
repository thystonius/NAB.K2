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
    /// Class used by Engines to provide information about columns returned by a query
    /// Used during design process
    /// </summary>
    public class DetectedColumn
    {
        /// <summary>
        /// Name of the column from the source
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Displayable name of the column
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Metadata long description of the column
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// .NET Type to use for returned value
        /// </summary>
        public Type ColumnType { get; set; }

        /// <summary>
        /// Indicates if this column contains data
        /// </summary>
        public bool ContainsData { get; set; }

        /// <summary>
        /// Source Type of the provided data
        /// </summary>
        public string InternalType { get; set; }

        /// <summary>
        /// Content Length reported for the first row (est. only)
        /// </summary>
        public int ContentLength { get; set; }

    }
}
