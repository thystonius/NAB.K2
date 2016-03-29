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
using System.Reflection;

using NAB.K2.SharePointSearch.Functions;

namespace NAB.K2.SharePointSearchEditor
{
    /// <summary>
    /// Singlton'ish class to load and cache the names of all the IProcessFunction classes so we don't have to get them each time
    /// SHOULD ONLY BE USED DURING DESIGN TIME for UI components, testing, etc.  At runtime these objects are cached by the Broker DLL.
    /// </summary>
    class DesignFunctionCache
    {
        private static Dictionary<string, IProcessFunction> _functions = null;
        private static List<DesignProcessFunctionDef> _defs = null;

        private static object _lockObject = new object();

        /// <summary>
        /// Retrieves a list of the cached Processing Functions
        /// </summary>
        public static List<IProcessFunction> AllFunctions { get { _loadCheck();  return _functions.Values.ToList(); } }


        /// <summary>
        /// Retrieves a list of the cached Processing Functions (And includes a blank record)
        /// </summary>
        public static List<DesignProcessFunctionDef> AllFunctionDefs { get { _loadCheck(); return _defs; } }
        
        /// <summary>
        /// Clears the cache and forces it to reload on next request for functions
        /// </summary>
        public static void Clear() { _functions = null; }

        /// <summary>
        /// Ensures that the static cache is loaded
        /// </summary>
        private static void _loadCheck()
        {
            if(_functions != null)
            {
                return;
            }

            lock(_lockObject)
            {
                if(_functions == null)
                {
                    _functions = new Dictionary<string, IProcessFunction>();
                    _defs = new List<DesignProcessFunctionDef>();

                    //Add a Blank row (for design purposes)
                    _defs.Add(new DesignProcessFunctionDef() { Name = string.Empty, TypeName = string.Empty });


                    //Get the type 
                    Type procFunc = typeof(IProcessFunction);

                    var a = Assembly.GetAssembly(procFunc);
                    foreach(Type t in a.GetExportedTypes())
                    {
                        //Only if it expsoses this interface
                        if(procFunc.IsAssignableFrom(t) && t != procFunc)
                        {
                            //Get an instance of the function
                            IProcessFunction f = Activator.CreateInstance(t) as IProcessFunction;
                            _functions.Add(t.Name, f);
                            _defs.Add(new DesignProcessFunctionDef() { Name = f.Name, TypeName = t.AssemblyQualifiedName });

                        }

                    }

                }

            }
            


        }

    }



    public class DesignProcessFunctionDef
    {
        public string Name { get; set; }
        public string TypeName { get; set; }

    }

}
