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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAB.K2.SharePointSearch.Functions
{
    /// <summary>
    /// Function class that will parse a Date stored as Text in the ISO 8601 format (yyyy-MM-ddTHH:mm:ssZ)
    /// </summary>
    public class DateTimeFromISO8601 : IProcessFunction
    {

        public const string FUNCTION_NAME = "Date Time from ISO8601 formated string";
        public const string FUNCTION_DESCRIPTION = @"Parses a Date Time stored as text in ISO 8601 format such as:
2016-01-13T21:37:34Z";

        #region Name & Description
        public string Name
        {
            get { return FUNCTION_NAME; }
        }

        public string Description
        {
            get { return FUNCTION_DESCRIPTION; }
        }
        #endregion

        
        public object ProcessValue(object input, string parameters)
        {
            string s = input as string;

            if(!string.IsNullOrWhiteSpace(s))
            {
                DateTime d2;
                if(DateTime.TryParse(s, null, DateTimeStyles.RoundtripKind, out d2))
                {
                    return d2;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }


        }

        
    }
}
