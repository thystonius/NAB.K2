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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using NAB.K2.SharePointSearch.Configuration;

namespace NAB.K2.SharePointSearch.Runtime
{
    /// <summary>
    /// Class used during runtime to pre-split a string that contains mergable macros / parameters so that it can be efficiently used at runtime
    /// This class is designed for use when a single string will be used multiple times during runtime
    /// 
    /// NOTE: Read through this code carefully as it was desired for runtime optimization and to reduce string concatenations
    /// </summary>
    public class MergableString
    {

        /// <summary>
        /// Indicates the start of a macro
        /// </summary>
        public const string MACRO_START = "%%{";

        /// <summary>
        /// Indicates the end of a macro
        /// </summary>
        public const string MACRO_END = "}%%";


        /// <summary>
        /// REGEX Patter the find all macros in a string
        /// </summary>
        public const string MACRO_REGEX = @"%%\{(\s*?.*?)*?\}%%";
        

        private readonly string _originalString;
        private bool _isMerged;

        private MatchCollection _macros = null;
        private List<MergeSnippit> _snippits = null;
        private int _preMergeLength;

        public MergableString(string template)
        {
            _originalString = template;

            //Get the macros contained in this string
            var matches = Regex.Matches(template, MACRO_REGEX);
            if(matches.Count == 0)
            {
                //None - so we are good to go
                _isMerged = false;
                return;

            }else
            {
                _isMerged = true;
                _macros = matches;

                //Next we split this string up into the parts to get merged back together
                _snippits = new List<MergeSnippit>();

                MergeSnippit snip;

                int curPos = 0;
                for(int i = 0; i < _macros.Count; i++)
                {
                    var match = _macros[i];

                    //Get the portion of the script between last macro and here
                    snip = new MergeSnippit();
                    snip.Snippit = _originalString.Substring(curPos, match.Index - curPos);
                    _snippits.Add(snip);
                    _preMergeLength += snip.Length;


                    //Now create one for the macro itself
                    //NOTE ALL MACROS ARE STORED IN UPPER CASE internally
                    //PARAMETERS AND SUCH MUST ALSO BE IN UPPER CASE OR MAKE SURE to Use CaseInsensitive string comparisons
                    _snippits.Add(MergeSnippit.CreateMacroSnip(match.Value.ToUpper()));


                    //Move to the next location
                    curPos = match.Index + match.Length;


                }

                //Check for tail
                if(curPos < _originalString.Length)
                {
                    snip = new MergeSnippit();
                    snip.Snippit = _originalString.Substring(curPos);
                    _snippits.Add(snip);
                }



            }


        }

        
        /// <summary>
        /// Method to marge the original source string with values from the IMacroValueProvider
        /// This methos is Thread Safe
        /// </summary>
        /// <param name="macros">IMacroValueProvider that will be queried for any macros found</param>
        /// <returns></returns>
        public string GenerateString(IMacroValueProvider macros)
        {

            //Short circuit if there are no macros
            if(!_isMerged)
            {
                return _originalString;
            }

            //Estimated new length of the string
            int totalLength = _preMergeLength + 256;


            //Array to hold the results (note, only the indexes for snippits that are macros are used)
            string[] values = new string[_snippits.Count];

            //First we gather all the macros
            //Note we do this so that we can allocate a string builder with the proper size
            for (int i = 0; i < _snippits.Count; i++ )
            {
                var sn = _snippits[i];

                //Only populate values for snippits that are macros
                if(sn.IsMacro)
                {
                    values[i] = macros.GetMacroValue(sn.Snippit);

                    if(!string.IsNullOrEmpty(values[i]))
                    {
                        totalLength += values[i].Length;
                    }
                    
                }

            }


            //Make the string builder with the proper size
            StringBuilder sb = new StringBuilder(totalLength);
            for (int i = 0; i < _snippits.Count; i++)
            {
                var sn = _snippits[i];

                if (sn.IsMacro)
                {
                    sb.Append(values[i]);
                }
                else
                {
                    sb.Append(sn.Snippit);
                }

            }

            //Finally return the completed string
            return sb.ToString();

        }

        /// <summary>
        /// Removes the macro tags from a macro
        /// </summary>
        /// <param name="macro"></param>
        /// <returns></returns>
        public static string StripMacro(string macro)
        {
            //WORK - WARNING does not check for proper tags, just does it by length - Not ideal
            return macro.Substring(MergableString.MACRO_START.Length, macro.Length - MergableString.MACRO_START.Length - MergableString.MACRO_END.Length);
        }


        /// <summary>
        /// Internal class used to store the pre-merged bits of a MergableString
        /// </summary>
        class MergeSnippit
        {

            public string Snippit { get; set; }
            public bool IsMacro { get; set; }
            public int Length { get { return Snippit.Length; } }

            public static MergeSnippit CreateMacroSnip(string macro)
            {
                MergeSnippit s = new MergeSnippit();
                s.IsMacro = true;

                s.Snippit = MergableString.StripMacro(macro);

                return s;
            }

        }


    }


    
    

}