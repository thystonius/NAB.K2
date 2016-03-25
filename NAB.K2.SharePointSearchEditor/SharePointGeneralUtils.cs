#region Copyright (c) 2015-2016 Nathan A. Brown
/**********************************************************************************
*   Copyright (C) 2015-2016 Nathan A. Brown - nab@thystonius.com
*   This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
*   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/ or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
*   All other rights reserved.
*   This work is considered proprietary.  Any use or right not covered in above license is considered reserved.
*   Use of this work or any derivative constitute an acceptance of all license terms and conditions.
*   
*   Project: NAB.K2.SharePointSearchEditor
*   Namespace: NAB.K2.SharePointSearchEditor
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAB.K2.SharePointSearchEditor
{
    class SharePointGeneralUtils
    {

        public const string SP_SPACE = "_x0020_";


        /// <summary>
        /// Makes column names in SharePoint lists pretty by removing any hex references
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string MakeFieldNamePretty(string name)
        {
            StringBuilder sb = new StringBuilder(name);

            //WORK - This is very rudementary and does not handle other characters
            sb.Replace(SP_SPACE, " ");

            return sb.ToString();

        }

    }
}
