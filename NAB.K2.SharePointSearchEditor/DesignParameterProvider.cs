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

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;

namespace NAB.K2.SharePointSearchEditor
{
    public class DesignParameterProvider : IParameterProvider
    {

        private QueryDef _Query;

        private Dictionary<string, string> _Values = new Dictionary<string, string>();


        public bool PrepareParameters(QueryDef q)
        {
            _Query = q;

            //Short circuit for no parameters
            if(_Query.Parameters.Count == 0)
            {
                return true;
            }


            foreach(var p in q.Parameters)
            {
                if(!p.IsCalculated)
                {
                    this.AddParameter(p.Name, p.DesignTimeValue);
                }
                
            }
            
            return PromptForValues();

        }


        public void AddParameter(string name, string value)
        {

            if(_Values.ContainsKey(name))
            {
                _Values[name] = value;

            }else
            {
                _Values.Add(name, value);
            }

            

        }

        
        public bool PropertyHasValue(string name)
        {
            return _Values.ContainsKey(name);
        }

        public string GetParameter(string name)
        {
            if (!_Values.ContainsKey(name))
            {
                //Ask for this parameter
                string value;

                value = string.Empty;

                _Values.Add(name, value);
            }


            return _Values[name].ToString();
        }


        public bool PromptForValues()
        {

            return frmParameterPrompt.PromptForParameters(this, _Query);
            
        }

    }
}
