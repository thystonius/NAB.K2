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
*   Namespace: NAB.K2.SharePointSearch.Functions
*   Written by: nathan.brown 
**********************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAB.K2.SharePointSearch.Functions
{
    /// <summary>
    /// Function class that will parse the SharePoint OWS User Field and extract the Full Name value
    /// Input is exacted as: jeanluc.picard@domain.com | Jean-Luc Picard | 6A3A302BBB777C6D756C746979999274735C6A6F686E6C73 i:0#.w|domain\jeanluc.picard
    /// </summary>
    public class OWSUserExtractUserFullName : IProcessFunction
    {

        public string Name
        {
            get { return "OWSUser Extract Full Name"; }
        }

        public string Description
        {
            get { return @"Parses SharePoint user identity string and extracts the user's Full Name.
This will parse fields such as:
jeanluc.picard@domain.com | Jean-Luc Picard | 6A3A302BBB777C6D756C746979999274735C6A6F686E6C73 i:0#.w|domain\jeanluc.picard"; }
        }


        public object ProcessValue(object input, string parameters)
        {
            string s = input as string;

            if(!string.IsNullOrWhiteSpace(s))
            {
                int i = s.IndexOf("|");
                int i2 = s.IndexOf("|", i + 1);

                if(i > 0 && i2 > i)
                {
                    return s.Substring(i + 1, i2 - i - 1).Trim();
                }
                else
                {
                    //Not sure what we have here?
                    return s;
                }

            }
            else
            {
                return null;
            }


        }

        
    }
}
