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
    /// Extensions to the Configuration Store class to keep that class as light and small as possible to facilitiy serialization and storeage
    /// </summary>
    public static class ConfigurationExtension
    {

        public static QueryDef GetQueryById(this ConfigurationStore store, string id)
        {

            var q = store.Queries.Where(p => p.QueryId == id).FirstOrDefault();
        
            return q;

        }

        public static List<QueryDef> GetQueryByFolder(this ConfigurationStore store, string folderId)
        {

            return store.Queries.Where(p => p.FolderId == folderId).ToList();

        }


         public static StoreFolder GetFolderById(this ConfigurationStore store, string id)
        {

            var f = store.Folders.Where(p => p.FolderId == id).FirstOrDefault();
        
            return f;

        }


    }
}
