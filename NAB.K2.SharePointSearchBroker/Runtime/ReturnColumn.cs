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
using System.Runtime.CompilerServices;

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Functions;

namespace NAB.K2.SharePointSearch.Runtime
{
    /// <summary>
    /// Class used by QueryRuntime for any pre-processing of columns to improve runtime efficiency
    /// </summary>
    public class ReturnColumn
    {
        private readonly QueryColumn _column;
        private readonly bool _IsProcessed;
        private readonly bool _IsMacro;

        /// <summary>
        /// Process Function object is instanciated once per-field that uses it
        /// </summary>
        private readonly IProcessFunction _ProcessFunction;

        private readonly MergableString _FunctionParam;

        public ReturnColumn(QueryColumn column)
        {
            _column = column;
            
            //Identify if this column requires pre-processing
            _IsProcessed = !string.IsNullOrWhiteSpace(_column.ProcessFunction);
            _IsMacro = false;


            if(_IsProcessed)
            {
                try
                {
                    Type t = Type.GetType(_column.ProcessFunction);
                    _ProcessFunction = (IProcessFunction)Activator.CreateInstance(t);
                
                }catch(Exception e)
                {
                    //For some reason we had a problem resolving or instanciating the IProcessFunction
                    //Most likely due to missing an assmebly

                    //Lets throw a most specific messsage to make it clear
                    throw new Exception(string.Format("Exception loading IProcessFunction {0} with exception {1}", _column.ProcessFunction, e.Message), e);
                }
                                
                if(!string.IsNullOrWhiteSpace(_column.ProcessFunctionParameter))
                {
                    //Create the mergable string
                    _FunctionParam = new MergableString(_column.ProcessFunctionParameter);

                    //Set the IsMacro if the mergable string is a macro
                    _IsMacro = _FunctionParam.IsMerged;
                }

            }
            else
            {
                _ProcessFunction = null;
            }

        }

        public QueryColumn Column { get { return _column; } }
        public bool IsProcessed { get { return _IsProcessed; } }

        /// <summary>
        /// Method to handle Post Processing of output column values
        /// </summary>
        /// <param name="column"></param>
        /// <param name="macros"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object ProcessColumnOutput(object value, IMacroValueProvider macros)
        {
            if (!IsProcessed)
            {
                return value;
            }

            if(_IsMacro)
            {
                return _ProcessFunction.ProcessValue(value, _FunctionParam.GenerateString(macros));
            }else{
                return _ProcessFunction.ProcessValue(value, _column.ProcessFunctionParameter);
            }          

            
        }



    }
}
