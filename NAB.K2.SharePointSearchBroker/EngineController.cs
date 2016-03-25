#region Copyright (c) 2012-2014 Multi Parts Supply, Inc.
/**********************************************************************************
*   Copyright (C) 2015 Multi Parts Supply, Inc.
*   All rights reserved.
*   Proprietary and confidential
*   
*   Project: MPS.K2.SharePointSearch
*   Namespace: MPS.K2.SharePointSearch.Runtime
*   Written by: nathan.brown 
*   Created on: 2015-5-17 at 8:19 AM
**********************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MPS.K2.SharePointSearch.Engines;

namespace MPS.K2.SharePointSearch.Runtime
{
    /// <summary>
    /// Controls the instanciation of the Query Engines
    /// ALL methods of this class are thread-safe
    /// </summary>
    public class EngineController
    {

        private static Dictionary<MPSK2QueryEngines, IQueryEngine> _engines = new Dictionary<MPSK2QueryEngines, IQueryEngine>();
        private static object _enginesSync = new object();

        public static IQueryEngine GetEngine(int type)
        {
            return GetEngine((MPSK2QueryEngines)type);
        }

        public static IQueryEngine GetEngine(MPSK2QueryEngines type)
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
                            case MPSK2QueryEngines.SPListCalmEngine:
                                _engines.Add(type, new SPCamlEngine());
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
