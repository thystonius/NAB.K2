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

using System.Text.RegularExpressions;

using NAB.K2.SharePointSearch;
using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;
using NAB.K2.SharePointSearchEditor.Utility;


namespace NAB.K2.SharePointSearchEditor
{
    public partial class frmQuery : Form
    {
        private ConfigurationStore _Store;
        private QueryDef _Query = new QueryDef();
        private bool _IsNew = false;

        private DesignParameterProvider _paramProvider = new DesignParameterProvider();

        #region Initialize

        public frmQuery()
        {
            InitializeComponent();
            
            cmbEngine.DataSource = Enum.GetValues(typeof(NABK2QueryEngines));


            colParamType.DataSource = Enum.GetValues(typeof(NABK2SoType));
            colParamType.ValueType = typeof(NABK2SoType);

            colColSMOType.DataSource = Enum.GetValues(typeof(NABK2SoType));
            colColSMOType.ValueType = typeof(NABK2SoType);


            _ColTrueFalse(colParamRequired);
            _ColTrueFalse(colParamIsCalculated);

            _ColTrueFalse(colColInlcude);
            

        }

        private void _ColTrueFalse(DataGridViewCheckBoxColumn c)
        {
            c.TrueValue = true;
            c.FalseValue = false;
        }


        private void frmQuery_Load(object sender, EventArgs e)
        {


            

        }

        #endregion
        
        #region Static Accessors
        public static bool NewQuery(ConfigurationStore store, string folderId, out QueryDef newQuery, Form parent)
        {

            frmQuery f = new frmQuery();
            f._Store = store;

            QueryDef q = new QueryDef();
            q.FolderId = folderId;
            q.MaxRecords = 100;
            q.QueryTimeoutSeconds = 10;
            q.CacheSeconds = 120;
            f._Query = q;

            f._Query.FolderId = folderId;
            f._IsNew = true;

            f._LoadForm();

            //Center the form
            FormUtility.CenterFormOnForm(f, parent);

            if (f.ShowDialog() == DialogResult.OK)
            {
                newQuery = f._Query;
                return true;
            }
            else
            {
                newQuery = null;
                return false;
            }

        }

        public static bool EditQuery(ConfigurationStore store, QueryDef q, Form parent)
        {
            frmQuery f = new frmQuery();
            f._Store = store;
            f._Query = q;
            f._LoadForm();

            //Init to the last tab
            f.tabProps.SelectedIndex = f.tabProps.TabCount - 1;

            //Center the form
            FormUtility.CenterFormOnForm(f, parent);


            if (f.ShowDialog() == DialogResult.OK)
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion


        #region Load & Save


        private void _LoadForm()
        {
            cmbFolder.DataSource = _Store.Folders;
            cmbFolder.ValueMember = "FolderId";
            cmbFolder.DisplayMember = "DisplayName";

            if(String.IsNullOrEmpty(_Query.FolderId))
            {
                cmbFolder.SelectedIndex = 0;
            }
            else
            {
                cmbFolder.SelectedValue = _Query.FolderId;
            }

            cmbEngine.SelectedItem = (NABK2QueryEngines)_Query.Engine;

            txtQueryId.Text = _Query.QueryId;
            txtName.Text = _Query.DisplayName;
            txtDescription.Text = _Query.Description;
            txtRows.Text = _Query.MaxRecords.ToString();
            txtTimeout.Text = _Query.QueryTimeoutSeconds.ToString();
            txtCacheSeconds.Text = _Query.CacheSeconds.ToString();
            txtURL.Text = _Query.RelativeUrl;
            txtSiteUrl.Text = _Query.RelativeSiteUrl;

            checkFQL.Checked = _Query.UseFQL;

            txtQuery.Text = _Query.QueryText;


            if(_IsNew)
            {
                this.Text = "Query: <new query>";

            }
            else
            {

                this.Text = "Query: " + _Query.QueryId;

            }

            foreach (var p in _Query.Parameters)
            {
                int index = gridParameters.Rows.Add();

                DataGridViewRow r = gridParameters.Rows[index];
                r.Cells[colParamId.Index].Value = p.Name;
                r.Cells[colParamName.Index].Value = p.DisplayName;
                r.Cells[colParamDescription.Index].Value = p.Description;

                r.Cells[colParamType.Index].Value = (NABK2SoType)p.SMOType;

                r.Cells[colParamRequired.Index].Value = p.IsRequired;
                r.Cells[colParamDefaultValue.Index].Value = p.DefaultValue;


                r.Cells[colParamIsCalculated.Index].Value = p.IsCalculated;
                r.Cells[colParamCalculation.Index].Value = p.Calculation;
                r.Cells[colParamDesignValue.Index].Value = p.DesignTimeValue;

            }


            //Load the Return Columns
            foreach (var c in _Query.Columns)
            {
                int index = gridColumns.Rows.Add();

                DataGridViewRow r = gridColumns.Rows[index];
                r.Cells[colColSource.Index].Value = c.SourceColumn;
                r.Cells[colColName.Index].Value = c.Name;
                r.Cells[colColDisplay.Index].Value = c.DisplayName;
                r.Cells[colColDescription.Index].Value = c.Description;

                r.Cells[colColDataType.Index].Value = c.ColumnType;
                r.Cells[colColInlcude.Index].Value = c.Include;

                r.Cells[colColSMOType.Index].Value = (NABK2SoType)c.SMOType;

            }


            //Load all the request columns
            foreach (var s in _Query.RequestColumns)
            {
                int index = gridRequestColumns.Rows.Add();

                DataGridViewRow r = gridRequestColumns.Rows[index];
                r.Cells[colReqColName.Index].Value = s;

            }


        }

        private void _QueryFromForm(QueryDef q)
        {
            //Basic Query Properties
            q.QueryId = txtQueryId.Text;
            q.DisplayName = txtName.Text;
            q.Description = txtDescription.Text;
            
            q.FolderId = (string)cmbFolder.SelectedValue;

            //Engine
            NABK2QueryEngines engine = NABK2QueryEngines.SPListCalmEngine;
            Enum.TryParse<NABK2QueryEngines>(cmbEngine.SelectedValue.ToString(), out engine);
            q.Engine = (int)engine;

            q.MaxRecords = Convert.ToInt32(txtRows.Text);
            q.QueryTimeoutSeconds = Convert.ToInt32(txtTimeout.Text);
            q.CacheSeconds = Convert.ToInt32(txtCacheSeconds.Text);
            q.RelativeUrl = txtURL.Text;
            q.RelativeSiteUrl = txtSiteUrl.Text;

            q.UseFQL = checkFQL.Checked;

            q.QueryText = txtQuery.Text;

            NABK2SoType soType;

            //Clear and reload columns and parameters
            q.Parameters.Clear();
            foreach(DataGridViewRow r in gridParameters.Rows)
            {
                if (!r.IsNewRow)
                {
                    QueryParameter p = new QueryParameter();

                    p.Name = (string)r.Cells[colParamId.Index].Value;
                    p.DisplayName = (string)r.Cells[colParamName.Index].Value;
                    p.Description = (string)r.Cells[colParamDescription.Index].Value;

                    if (r.Cells[colParamType.Index].Value != null)
                    {
                        Enum.TryParse<NABK2SoType>(r.Cells[colParamType.Index].Value.ToString(), out soType);
                        p.SMOType = (int)soType;
                    }

                    p.IsRequired = Convert.ToBoolean(r.Cells[colParamRequired.Index].Value);
                    p.DefaultValue = (string)r.Cells[colParamDefaultValue.Index].Value;

                    p.IsCalculated = Convert.ToBoolean(r.Cells[colParamIsCalculated.Index].Value);
                    p.Calculation = (string)r.Cells[colParamCalculation.Index].Value;

                    p.DesignTimeValue = (string)r.Cells[colParamDesignValue.Index].Value;

                    //Add
                    q.Parameters.Add(p);
                }
            }
                        
            
            q.Columns.Clear();
            foreach (DataGridViewRow r in gridColumns.Rows)
            {
                if(!r.IsNewRow)
                { 
                    QueryColumn c = new QueryColumn();
                
                    c.SourceColumn = (string)r.Cells[colColSource.Index].Value;
                    c.Name = (string)r.Cells[colColName.Index].Value;
                    c.DisplayName = (string)r.Cells[colColDisplay.Index].Value;
                    c.Description = (string)r.Cells[colColDescription.Index].Value;

                    c.ColumnType = (string)r.Cells[colColDataType.Index].Value;

                    c.Include = Convert.ToBoolean(r.Cells[colColInlcude.Index].Value);

                    Enum.TryParse<NABK2SoType>(r.Cells[colColSMOType.Index].Value.ToString(), out soType);
                    c.SMOType = (int)soType;

                    //Add
                    q.Columns.Add(c);                   
                }

            }

            //Get all the request columns
            q.RequestColumns.Clear();
            foreach (DataGridViewRow r in gridRequestColumns.Rows)
            {
                if (!r.IsNewRow)
                {
                    //foreach (var s in _Query.RequestColumns)
                    q.RequestColumns.Add((string)r.Cells[colReqColName.Index].Value);
                }
                
            }
            

        }

        /// <summary>
        /// Basic qurey validation and displays message
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        private bool _ValidateQuery(QueryDef q)
        {
            List<string> messages;
            if (!q.ValidateQuery(out messages))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("There are the following configuration errors with this query:\r\n");
                messages.ForEach(s => { sb.Append(s); sb.Append("\r\n"); });
                MessageBox.Show(sb.ToString());

                return false;
            }
            else
            {
                return true;
            }
        }


        #endregion

        #region Button Events

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (_IsNew)
            {
                //Check for duplicate ID
                if(_Store.GetQueryById(txtQueryId.Text) != null )
                {
                    MessageBox.Show("Query Id must be unique.");
                    txtQueryId.Focus();
                    return;

                }

            }

            //Load into temp query & Validate
            QueryDef q = new QueryDef();
            _QueryFromForm(q);
            if (!_ValidateQuery(q))
            {
                return;
            }
                

            //Load back into main query object
            _QueryFromForm(_Query);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();


        }


        private void cmbFolder_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {

            QueryDef q = new QueryDef();
            _QueryFromForm(q);
            if (!_ValidateQuery(q))
            {
                return;
            }

            //Perform the test
            if(frmTest.TestQuery(q, _paramProvider, this) != DataGridViewTriState.False)
            {
                //As long as the didn't cancel, then save the design values for next time
                //Use Case = If they succeeed or error out then they do not want to re-enter parameters; Cancel = I may need to alter something before I save

                string name;
                
                //OPT - This is an innefficient loop and could be improved
                //Loop through the rows 
                foreach (DataGridViewRow r in gridParameters.Rows)
                {

                    name = (string)r.Cells[colParamId.Index].Value;
                    
                    foreach(var p in q.Parameters)
                    {
                        if(p.Name == name)
                        {
                            //Save back into the grid
                            r.Cells[colParamDesignValue.Index].Value = p.DesignTimeValue;
                            break;
                        }
                    }
                    
                }


            }

        }

        /// <summary>
        /// Inserts a new parameter at the current cursor position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddParam_Click(object sender, EventArgs e)
        {

            int cursorPos = txtQuery.SelectionStart;

            //Just insert the tag
            txtQuery.Text = txtQuery.Text.Insert(cursorPos, MergableString.MACRO_START + MergableString.MACRO_END);
            txtQuery.Focus();
            txtQuery.SelectionStart = cursorPos + MergableString.MACRO_START.Length;
            txtQuery.SelectionLength = 0;


        }

        private void buttonColumnsAll_Click(object sender, EventArgs e)
        {
            _solumnsSelect(true);
        }

        private void buttonColumnsNone_Click(object sender, EventArgs e)
        {
            _solumnsSelect(false);
        }

        private void _solumnsSelect(bool selectYesNo)
        {

            foreach (DataGridViewRow r in gridColumns.Rows)
            {
                if (!r.IsNewRow)
                {
                    r.Cells[colColInlcude.Index].Value = selectYesNo;
                }

            }

        }

        #endregion

        #region Detect Columns

        private void buttonClearColumns_Click(object sender, EventArgs e)
        {
            //Ask to clear
            if (MessageBox.Show("This will remove all existing colum definitions  Are you sure you want to proceed?", "Clear Columns", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                //Clear them
                gridColumns.Rows.Clear();
            }
        }


        private void buttonDetectCols_Click(object sender, EventArgs e)
        {

            //Get a copy of the current state
            QueryDef q = new QueryDef();

            IQueryEngine engine;
            IRuntimeConnection conn;
            QueryRuntime qr;
            RuntimeMacroProvider rmp;

            try
            {
                _QueryFromForm(q);

                if(!_ValidateQuery(q))
                {
                    return;
                }

                _paramProvider.PrepareParameters(q);

                //Get the engine
                engine = EngineController.GetEngine(q.Engine);
                conn = DesignConnection.GetGlobalConnection();
                qr = conn.GetQueryRuntime(q);
                rmp = new RuntimeMacroProvider(qr, _paramProvider);


            }catch(Exception ex)
            {
                MessageBox.Show("Exception preparing to detect query columns: " + ex.Message + "\r\nThis is typicaly caused by invalid query column definitions, please try removing or checking your current columns");
                return;
            }


            List<ReturnColumn> cols;
            try
            {
                cols = engine.RetrieveOutputColumns(conn, rmp, qr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception retrieving columns: {0} - {1}", ex.GetType(), ex.Message));
                return;
            }

            if (cols.Count == 0)
            {
                MessageBox.Show("No columns were returned.");
                return;
            }

            if (gridColumns.RowCount > 0)
            {
                //Ask to overwrite
                if (MessageBox.Show("This will overwrite existing columns.  Are you sure you want to proceed?", "Overwrite columns", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                //Clear them
                gridColumns.Rows.Clear();

            }
            


            foreach(var c in cols)
            {
                int index = gridColumns.Rows.Add();

                DataGridViewRow r = gridColumns.Rows[index];
                r.Cells[colColSource.Index].Value = c.Name;

                r.Cells[colColName.Index].Value = c.Name;
                r.Cells[colColDisplay.Index].Value = c.DisplayName;
                r.Cells[colColDescription.Index].Value = c.Description;

                r.Cells[colColDataType.Index].Value = c.InternalType;
                r.Cells[colColInlcude.Index].Value = c.ContainsData;

                //Lastly we have to propose a SO type for c.ColumName
                r.Cells[colColSMOType.Index].Value = TypeMapper.ProposeSOType(c.ColumnType.Name);

            }


        }

        #endregion
              
        #region Detect Parameters
        
        private void buttonDetectParams_Click(object sender, EventArgs e)
        {

            //Ask to overwrite
            if (MessageBox.Show("This will remove any non-used parameters.  Are you sure you want to proceed?", "Overwrite parameters", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

             QueryDef q = new QueryDef();
            _QueryFromForm(q);

            Dictionary<string, string> _macros = new Dictionary<string, string>();

            //Create the regex pattern
            Regex reg = new Regex(MergableString.MACRO_REGEX);
            
            //Detect parameters
            DetectParams_AddMatches(reg.Matches(q.QueryText), _macros);
            DetectParams_AddMatches(reg.Matches(q.RelativeUrl), _macros);
             

            //Remove any parameters that we didn't find
            foreach(DataGridViewRow r in gridParameters.Rows)
            {
                if (!r.IsNewRow)
                {
                    string key = MergableString.MACRO_START + r.Cells[colParamId.Index].Value + MergableString.MACRO_END;

                    if (!_macros.ContainsKey(key))
                    {
                        //This parameter is no longer used so remove
                        gridParameters.Rows.Remove(r);
                    }
                    else
                    {
                        //Does exist, so remove key from list of macros t
                        _macros.Remove(key);
                    }
                }

            }

            
            foreach(var c in _macros.Values)
            {
                int index = gridParameters.Rows.Add();

                DataGridViewRow r = gridParameters.Rows[index];
                r.Cells[colParamId.Index].Value = c;
                r.Cells[colParamName.Index].Value = c;
                r.Cells[colParamDescription.Index].Value = string.Empty;

                r.Cells[colParamType.Index].Value = NABK2SoType.Text;

                r.Cells[colParamRequired.Index].Value = false;
                r.Cells[colParamDefaultValue.Index].Value = string.Empty;


                r.Cells[colParamIsCalculated.Index].Value = false;
                r.Cells[colParamCalculation.Index].Value = string.Empty;                


            }



        }
        
        private void DetectParams_AddMatches(MatchCollection matches, Dictionary<string, string> macros)
        {
            for(int i = 0; i < matches.Count; i++)
            {
                var m = matches[i];

                if(!macros.ContainsKey(m.Value))
                {
                    macros.Add(m.Value, MergableString.StripMacro(m.Value));
                }
            }

        }

        #endregion

        #region Misc Control Events

        private void gridColumns_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Ignore for the moment
        }

        private void txtCacheSeconds_TextChanged(object sender, EventArgs e)
        {

            try
            {
                int i = Convert.ToInt32(txtCacheSeconds.Text);


                lblCacheResult.Text = (new TimeSpan(0, 0, i)).ToString("c");
                return;

            }catch(Exception)
            {

                //Swallow it                

            }

            lblCacheResult.Text = "--:--:--";


        }

        private void txtCacheSeconds_Leave(object sender, EventArgs e)
        {
            txtCacheSeconds_TextChanged(sender, e);

        }


        #endregion

        #region Generate CAML

        private void buttonMakeCAML_Click(object sender, EventArgs e)
        {

            //Check for existing query
            if (!string.IsNullOrEmpty(txtQuery.Text))
            {
                if (MessageBox.Show("This will overwrite your existing Query Text.  Are you sure you want to proceed?", "Overwrite Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    //They do not want to continue
                    return;
                }
            }


            //Get a copy of the current state
            QueryDef q = new QueryDef();
            _QueryFromForm(q);
            _paramProvider.PrepareParameters(q);

            //Set Empty Query
            q.QueryText = "<View><Query></Query></View>";

            //Get the engine
            IQueryEngine engine = EngineController.GetEngine(q.Engine);

            IRuntimeConnection conn = DesignConnection.GetGlobalConnection();
            QueryRuntime qr = conn.GetQueryRuntime(q);

            RuntimeMacroProvider rmp = new RuntimeMacroProvider(qr, _paramProvider);

            List<ReturnColumn> cols;
            try
            {
                cols = engine.RetrieveOutputColumns(conn, rmp, qr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception retrieving columns: {0} - {1}", ex.GetType(), ex.Message));
                return;
            }

            StringBuilder sb = new StringBuilder();

            //Open The query
            sb.Append("<View><Query>\r\n");

            //Add a where first (only as comment)
            sb.Append("   <!-- SAMPLE WHERE \r\n<Where><Eq><FieldRef Name='Title' /><Value Type='String'>some title</Value></Eq></Where> \r\n -->\r\n");

            //Add an order by of the first field - to show format
            sb.Append("   <OrderBy UseIndexForOrderBy='TRUE'><FieldRef Ascending='TRUE' Name='");
            sb.Append(cols[0].Name);
            sb.Append("' /></OrderBy>\r\n");


            sb.Append("   <ViewFields>\r\n");
            foreach (var c in cols)
            {
                sb.Append(string.Format("      <FieldRef Name='{0}' />\r\n", c.Name));
            }

            sb.Append("   </ViewFields>\r\n");
            sb.Append("</Query></View>");


            txtQuery.Text = sb.ToString();

        }
        
        #endregion


        #region Help Buttons

        private void buttonHelpKQL_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("https://msdn.microsoft.com/en-us/library/office/jj163973.aspx");

        }

        private void buttonHelpCAML_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://msdn.microsoft.com/en-us/library/office/ms462365.aspx");

        }
        
        #endregion

       
        

    }
}
