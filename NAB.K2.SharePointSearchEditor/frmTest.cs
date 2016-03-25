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
using NAB.K2.SharePointSearch.Runtime;
using NAB.K2.SharePointSearchEditor.Utility;
using System.Diagnostics;



namespace NAB.K2.SharePointSearchEditor
{
    public partial class frmTest : Form
    {

        private IQueryEngine _engine;
        private IRuntimeConnection _conn;
        private QueryRuntime _qr;
        private RuntimeMacroProvider _rmp;


        public frmTest()
        {
            InitializeComponent();
        }
    

        public static DataGridViewTriState TestQuery(QueryDef query, DesignParameterProvider paramProvider, Form parent)
        {

            //First get the parameters (this is their only opporunity to cancel)
            if(!paramProvider.PrepareParameters(query))
            {
                return DataGridViewTriState.False;
            }
            
            frmTest f = new frmTest();

            try
            {
                //Set the values
                f._engine = EngineController.GetEngine(query.Engine);
                f._conn = DesignConnection.GetGlobalConnection();
                f._qr = f._conn.GetQueryRuntime(query);
                f._rmp = new RuntimeMacroProvider(f._qr, paramProvider);

            }catch(Exception e)
            {
                MessageBox.Show("Exception setting up Query: " + e.Message, "Runtime configuration error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return DataGridViewTriState.False;
            }
            
            if(!f.RunQuery())
            {
                f.Close();
                return DataGridViewTriState.NotSet;
            }


            FormUtility.CenterFormOnForm(f, parent);

            //Show the results
            f.ShowDialog();

            //Return ok
            return DataGridViewTriState.True;

        }


        /// <summary>
        /// Runs the query and records the time
        /// </summary>
        /// <returns></returns>
        private bool RunQuery()
        {

            try
            {
                Stopwatch sw = new Stopwatch();

                sw.Start();

                gridResults.DataSource = _engine.ExecuteToDataTable(_conn, _rmp, _qr);

                sw.Stop();

                labelTime.Text = string.Format("Took: {0} seconds.", sw.Elapsed.TotalSeconds.ToString("0.0##"));

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception testing query: {0} - {1}", ex.GetType(), ex.Message));
                return false;
            }

        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            RunQuery();
        }

    }
}
