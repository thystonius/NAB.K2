﻿#region Copyright (c) 2015-2016 Nathan A. Brown
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
    /// Base class for both Columns and Parameters
    /// Usinga base class here so facilitate easier metadata extraction
    /// </summary>
    public abstract class PropertyBase
    {

        /// <summary>
        /// Internal Name of this property / parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        public string DisplayName { get; set; }
                
        /// <summary>
        /// Metadata Description
        /// </summary>
        public string Description { get; set; }
        

        /// <summary>
        /// Smart Object Type
        /// </summary>
        public int SMOType { get; set; }


    }
}
