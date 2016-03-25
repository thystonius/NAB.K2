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
    [Serializable]
    public class QueryRuntimeException : Exception
    {
        public const string MESSAGE_InvalidParameter = "Invalid or missing parameter definition of [{0}] for query {1}.";
        public const string MESSAGE_MissingParameter = "Required parameter [{0}] not provided for query {1}.";


        public QueryRuntimeException() { }

        public QueryRuntimeException(string message) : base(message)
        {

        }


        public static QueryRuntimeException InvalidParameter(string QueryId, string parameterName)
        {
            return new QueryRuntimeException(string.Format(MESSAGE_InvalidParameter,parameterName, QueryId));
        }


        public static QueryRuntimeException MissingParameter(string QueryId, string parameterName)
        {
            return new QueryRuntimeException(string.Format(MESSAGE_MissingParameter, parameterName, QueryId));
        }


    }
}
