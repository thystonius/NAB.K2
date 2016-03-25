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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAB.K2.SharePointSearch.Configuration;

namespace NAB.K2.SharePointSearchEditor
{
    public partial class frmParameterPrompt : Form
    {
        public frmParameterPrompt()
        {
            InitializeComponent();
        }

        private QueryDef _query;
        private DesignParameterProvider _provider;

        public static bool PromptForParameters(DesignParameterProvider provider, QueryDef query)
        {
            frmParameterPrompt f = new frmParameterPrompt();

            f._query = query;
            f._provider = provider;

            f.LoadForm();

            return f.ShowDialog() == DialogResult.OK;

        }

        private void LoadForm()
        {

            //Load the grid
            foreach (var p in _query.Parameters)
            {
                int index = gridParameters.Rows.Add();

                DataGridViewRow r = gridParameters.Rows[index];
                r.Cells[colParamId.Index].Value = p.Name;
                r.Cells[colParamName.Index].Value = p.DisplayName;

                if(string.IsNullOrEmpty(p.DesignTimeValue))
                {
                    r.Cells[colParamValue.Index].Value = p.DefaultValue;
                }
                else
                {
                    r.Cells[colParamValue.Index].Value = p.DesignTimeValue;
                }

                

            }




        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            string name;
            string value;


            foreach (DataGridViewRow r in gridParameters.Rows)
            {

                name = (string)r.Cells[colParamId.Index].Value;
                value = (string)r.Cells[colParamValue.Index].Value;

                if(!string.IsNullOrEmpty(name))
                {
                    _provider.AddParameter(name, value);
                }                

            }


            //Also save these values back into the query as design time values
            foreach(var p in _query.Parameters)
            {
                if(!p.IsCalculated)
                {
                    p.DesignTimeValue = _provider.GetParameter(p.Name);
                }
                
            }



            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        


    }
}
