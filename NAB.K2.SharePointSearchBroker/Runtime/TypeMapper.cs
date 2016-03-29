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
    public class TypeMapper
    {

        public const string SPCAML_DATE_FORMAT = "yyyy-MM-ddThh:nn:ssZ";


        private static Dictionary<string, Tuple<NABK2SoType, Type>> _standardTypeMap = null;
        private static Dictionary<NABK2SoType, Type> _standardSOMap = null;
        private static object _standardTypeMapSync = new object();

        /// <summary>
        /// Determines and assigns a Type for an object
        /// This is used to potentially override any runtime types that may not pass through K2 well and convert them
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        public static Type DecipherObjectType(object fromObject)
        {
            if (fromObject == null)
            {
                return typeof(string);
            }
            else
            {
                return fromObject.GetType();
            }

        }


        public static Type GetTypeForSO(NABK2SoType soType)
        {

            if(_standardSOMap == null)
            {
                //Force load of the dictionaries
                NABK2SoType t = ProposeSOType("string", 0);
            }

            //Return the type
            return _standardSOMap[soType];


        }


        /// <summary>
        /// Suggests a SO Type
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static NABK2SoType ProposeSOType(string typeName, int length)
        {
            if (_standardTypeMap == null)
            {
                _LoadStandardTypes();
            }

            string tn = typeName.ToLower();

            if (_standardTypeMap.ContainsKey(tn))
            {
                NABK2SoType output = _standardTypeMap[tn].Item1;
                if(output == NABK2SoType.Text)
                {
                    if(length > 1024)
                    {
                        output = NABK2SoType.Memo;
                    }

                }

                return output;
            }
            else
            {
                //Run home to mama (default to text)
                return NABK2SoType.Text;
            }
        }



        private static void _LoadStandardTypes()
        {
            if (_standardTypeMap != null)
                return;

            lock (_standardTypeMapSync)
            {
                if (_standardTypeMap == null)
                {

                    Dictionary<string, Tuple<NABK2SoType, Type>> map = new Dictionary<string, Tuple<NABK2SoType, Type>>();


                    map.Add("datetime", new Tuple<NABK2SoType, Type>(NABK2SoType.DateTime, typeof(DateTime)));

                    map.Add("string", new Tuple<NABK2SoType, Type>(NABK2SoType.Text, typeof(string)));

                    map.Add("byte", new Tuple<NABK2SoType, Type>(NABK2SoType.Number, typeof(byte)));
                    map.Add("short", new Tuple<NABK2SoType, Type>(NABK2SoType.Number, typeof(short)));
                    map.Add("int", new Tuple<NABK2SoType, Type>(NABK2SoType.Number, typeof(int)));
                    map.Add("long", new Tuple<NABK2SoType, Type>(NABK2SoType.Number, typeof(long)));

                    map.Add("int16", new Tuple<NABK2SoType, Type>(NABK2SoType.Number, typeof(Int16)));
                    map.Add("int32", new Tuple<NABK2SoType, Type>(NABK2SoType.Number, typeof(Int32)));
                    map.Add("int64", new Tuple<NABK2SoType, Type>(NABK2SoType.Number, typeof(Int64)));


                    map.Add("single", new Tuple<NABK2SoType, Type>(NABK2SoType.Decimal, typeof(Single)));
                    map.Add("double", new Tuple<NABK2SoType, Type>(NABK2SoType.Decimal, typeof(Double)));
                    map.Add("decimal", new Tuple<NABK2SoType, Type>(NABK2SoType.Decimal, typeof(Decimal)));


                    map.Add("guid", new Tuple<NABK2SoType, Type>(NABK2SoType.Guid, typeof(Guid)));

                    map.Add("bool", new Tuple<NABK2SoType, Type>(NABK2SoType.YesNo, typeof(bool)));
                    map.Add("boolean", new Tuple<NABK2SoType, Type>(NABK2SoType.YesNo, typeof(Boolean)));

                    //Be sure to only set thred synced objects just before releasing lock
                    _standardTypeMap = map;


                    //Dictionary of reverse maps
                    Dictionary<NABK2SoType, Type> soMap = new Dictionary<NABK2SoType,Type>();

                    soMap.Add(NABK2SoType.Text , typeof(string));
                    soMap.Add(NABK2SoType.Memo , typeof(string));
                    soMap.Add(NABK2SoType.Number , typeof(long));
                    soMap.Add(NABK2SoType.Decimal , typeof(decimal));
                    soMap.Add(NABK2SoType.Autonumber , typeof(long));
                    soMap.Add(NABK2SoType.YesNo , typeof(bool));
                    soMap.Add(NABK2SoType.DateTime , typeof(DateTime));
                    soMap.Add(NABK2SoType.Image , typeof(string));
                    soMap.Add(NABK2SoType.File , typeof(string));
                    soMap.Add(NABK2SoType.MultiValue , typeof(string));
                    soMap.Add(NABK2SoType.Xml , typeof(string));
                    soMap.Add(NABK2SoType.Default , typeof(string));
                    soMap.Add(NABK2SoType.HyperLink , typeof(string));
                    soMap.Add(NABK2SoType.Guid , typeof(Guid));
                    soMap.Add(NABK2SoType.AutoGuid , typeof(Guid));
                    soMap.Add(NABK2SoType.Date , typeof(DateTime));
                    soMap.Add(NABK2SoType.Time , typeof(DateTime));
                                        

                    _standardSOMap = soMap;
                }

            }



        }


    }
}
