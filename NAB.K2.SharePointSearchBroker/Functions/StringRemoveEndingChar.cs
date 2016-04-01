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
    /// Function class that will remove a trailing character from a column
    /// </summary>
    public class StringRemoveEndingChar : IProcessFunction
    {

        public const string FUNCTION_NAME = "String Remove Training Value";
        public const string FUNCTION_DESCRIPTION = @"Removes the training value specified in parameter from the string";

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
            //If there is no parameter, then just return
            if(string.IsNullOrWhiteSpace(parameters))
            {
                return input;
            }

            string s = input as string;

            if(!string.IsNullOrWhiteSpace(s))
            {
                if(s.EndsWith(parameters))
                {
                    return s.Substring(0, s.Length - parameters.Length);
                }
                else
                {
                    return input;
                }

            }
            else
            {
                return null;
            }


        }

        
    }
}
