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
*   Namespace: NAB.K2.SharePointSearch.Configuration
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

using NAB.K2.SharePointSearch.Runtime;

using Newtonsoft.Json;


namespace NAB.K2.SharePointSearch.Configuration
{
    public static class QueryExtension
    {

        /// <summary>
        /// Clones the QueryDef making deep copies of all values
        /// </summary>
        /// <param name="query">Query to clone</param>
        /// <returns>new QueryDef object</returns>
        public static QueryDef Clone(this QueryDef query)
        {
            //Just perform simple serialization to make deep copy
            string json = JsonConvert.SerializeObject(query);
            return JsonConvert.DeserializeObject<QueryDef>(json);

        }
        

        public static DataTable BuildDataTable(this QueryDef query)
        {

            DataTable dt = new DataTable();

            //Add the columns
            foreach (var col in query.Columns)
            {

                DataColumn dc = dt.Columns.Add();
                dc.ColumnName = col.Name;
                dc.Caption = col.DisplayName;
                dc.DataType = TypeMapper.GetTypeForSO((NABK2SoType)col.SMOType);

                switch((NABK2SoType)col.SMOType)
                {
                    case NABK2SoType.Text:
                        dc.MaxLength = 255;
                        break;
                    case NABK2SoType.Memo:
                        dc.MaxLength = 32766;
                        break;
                }
                

            }


            return dt;

        }


        /// <summary>
        /// Performs basic validation that the query configuratoin is sound
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool ValidateQuery(this QueryDef query, out List<string> errorMessages)
        {
            bool IsOk = true;
            List<string> messages = new List<string>();

            var dupes = query.Columns.GroupBy(p => p.Name).Where(g => g.Count() > 1).Select(r => r.Key).ToList();
            if(dupes.Count > 0)
            {
                dupes.ForEach(p => messages.Add(string.Format("Duplicate output column name {0}", p)));
                IsOk = false;
            }


            if(query.MaxRecords <= 0)
            {
                messages.Add("Max Rows must be greater than zero.");
                IsOk = false;
            }


            errorMessages = messages;
            return IsOk;
        }


    }
}
