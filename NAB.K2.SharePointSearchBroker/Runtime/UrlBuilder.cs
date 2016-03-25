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
    /// Dirt simple url builder, no checking other than slashes
    /// </summary>
    internal class UrlBuilder
    {
        private const string SLASH = "/";

        private StringBuilder sb;

        public UrlBuilder(string baseUrl)
        {

            sb = new StringBuilder(baseUrl, baseUrl.Length * 3);

            if(!baseUrl.EndsWith(SLASH))
            {
                sb.Append(SLASH);
            }


        }


        public void AppendPath(string path)
        {
            //Short circuit for empty
            if(string.IsNullOrEmpty(path))
            {
                return;
            }

            if(path.StartsWith(SLASH))
            {
                sb.Append(path, 1, path.Length - 1);
            }
            else
            {
                sb.Append(path);
            }

            if(!path.EndsWith(SLASH))
            {
                sb.Append(SLASH);
            }

        }



        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
