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

using NAB.K2.SharePointSearch.Configuration;

namespace NAB.K2.SharePointSearch.Runtime
{

    /// <summary>
    /// Class provides central location for retriving and merging all runtime macros
    /// </summary>
    public class RuntimeMacroProvider : IMacroValueProvider
    {

        private readonly QueryRuntime _qRuntime;
        private readonly IParameterProvider _parameters;


        public RuntimeMacroProvider(QueryRuntime qRuntime, IParameterProvider parameters)
        {
            _qRuntime = qRuntime;
            _parameters = parameters;
        }


        /// <summary>
        /// Retrieves the macro value
        /// </summary>
        /// <param name="name">Name of the macro</param>
        /// <returns></returns>
        public string GetMacroValue(string name)
        {
            
            //First we get the parameter from the query
            QueryParameter qp = _qRuntime.GetParameter(name);

            //Make sure that we have a definition for each parameter - ALL PARAMETERS MUST BE DEFINED!!!
            if(qp == null)
            {
                throw QueryRuntimeException.InvalidParameter(_qRuntime.QueryId, name);
            }


            //Check for calculated value
            if(qp.IsCalculated)
            {

                //Perform calculation

                //WORK - calcuation engine (NOT YET COMPLETE)
                return string.Empty;

            }
            

            //Look for a values from the property box
            if (_parameters.PropertyHasValue(name))
            {
                return _parameters.GetParameter(name);
            }
            else
            {
                //Check for required & default value
                if (qp.IsRequired)
                {
                    //Missing a required parameter value throws an exception!
                    throw QueryRuntimeException.MissingParameter(_qRuntime.QueryId, name);
                }
                else
                {
                    return qp.DefaultValue;
                }

            }
            

        }


        public string GetValueRaw(string name)
        {
            if (_parameters.PropertyHasValue(name))
            {
                return _parameters.GetParameter(name);
            }
            else
            {
                return null;
            }
        }


    }
}
