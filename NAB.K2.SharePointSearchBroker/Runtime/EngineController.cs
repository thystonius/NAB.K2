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

using NAB.K2.SharePointSearch.Engines;

namespace NAB.K2.SharePointSearch.Runtime
{
    /// <summary>
    /// Controls the instanciation of the Query Engines
    /// ALL methods of this class are thread-safe
    /// </summary>
    public class EngineController
    {

        private static Dictionary<NABK2QueryEngines, IQueryEngine> _engines = new Dictionary<NABK2QueryEngines, IQueryEngine>();
        private static object _enginesSync = new object();

        public static IQueryEngine GetEngine(int type)
        {
            return GetEngine((NABK2QueryEngines)type);
        }

        public static IQueryEngine GetEngine(NABK2QueryEngines type)
        {
            //Lazy Load the engines
            if(!_engines.ContainsKey(type))
            {
                lock(_enginesSync)
                {
                    if(!_engines.ContainsKey(type))
                    {
                        //WORK - for now lets just use a simple switch to load types (there are only several)
                        switch(type)
                        {
                            case NABK2QueryEngines.SPListCalmEngine:
                                _engines.Add(type, new SPCamlEngine());
                                break;

                            case NABK2QueryEngines.SPSearchEngine:
                                _engines.Add(type, new SPSearchEngine());
                                break;
                        }

                    }

                }
            }

            //Return the engine
            return _engines[type];

        }
        


    }
}
