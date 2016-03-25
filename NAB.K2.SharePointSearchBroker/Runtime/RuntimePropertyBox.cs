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

using K2Objects = SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using K2Type = SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace NAB.K2.SharePointSearch.Runtime
{
    class RuntimePropertyBox : IParameterProvider
    {

        private Dictionary<string, Tuple<K2Objects.Property, K2Objects.MethodParameter>> _props;


        public RuntimePropertyBox()
        {
            //This is using the invariant culture and case-insensitive.
            //This may not be exactly the same method that K2 uses, so if K2 switches to use case sensitive parameter names, then this may not be valid
            _props = new Dictionary<string, Tuple<K2Objects.Property, K2Objects.MethodParameter>>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void AddParameter(K2Objects.Property property)
        {
            _props.Add(property.Name, new Tuple<K2Objects.Property, K2Objects.MethodParameter>(property, null) );

        }

        public void AddParameter(K2Objects.MethodParameter parameter)
        {

            _props.Add(parameter.Name, new Tuple<K2Objects.Property, K2Objects.MethodParameter>(null, parameter));

        }

        
        public bool PropertyHasValue(string name)
        {
            if(_props.ContainsKey(name))
            {
                var x = _props[name];

                if(x.Item1 != null)
                {
                    return !x.Item1.IsClear;
                }else
                {
                    return !x.Item2.IsClear;
                }

            }
            else
            {
                return false;
            }

        }



        
        public string GetParameter(string name)
        {
            
            if (_props.ContainsKey(name))
            {
                var x = _props[name];
                
                if(x.Item1 != null)
                {
                    var p = x.Item1;
                    if(p.IsClear)
                    {
                        return null;
                    }
                    else
                    {
                        var soType = p.SoType;

                        //Check for date / time value
                        if (soType == K2Type.SoType.DateTime || soType == K2Type.SoType.Date || soType == K2Type.SoType.Time)
                        {
                            return ((DateTime)p.Value).ToString(TypeMapper.SPCAML_DATE_FORMAT);
                        }
                        else
                        {
                            if(p.Value != null)
                            {
                                return p.Value.ToString();
                            }
                            else
                            {
                                return string.Empty;
                            }

                        }
                    
                    }
                }else{
                    var m = x.Item2;
                    if(m.IsClear)
                    {
                        return null;
                    }
                    else
                    {
                        var soType = m.SoType;

                        //Check for date / time value
                        if (soType == K2Type.SoType.DateTime || soType == K2Type.SoType.Date || soType == K2Type.SoType.Time)
                        {
                            return ((DateTime)m.Value).ToString(TypeMapper.SPCAML_DATE_FORMAT);
                        }
                        else
                        {
                            if(m.Value != null)
                            {
                                return m.Value.ToString();
                            }
                            else
                            {
                                return string.Empty;
                            }

                        }
                    
                    }

                }


                
            }
            else
            {


                return null;
            }

            
        }




    }
}
