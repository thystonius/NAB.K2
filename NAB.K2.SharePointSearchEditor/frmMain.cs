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
using System.Diagnostics;
using System.IO;
using System.Configuration;

using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearch.Runtime;

using NAB.K2.SharePointSearchEditor.Utility;

namespace NAB.K2.SharePointSearchEditor
{
    public partial class frmMain : Form
    {

        private const string IMG_FOLDER = "folder";
        private const string IMG_CONFIG = "page";
        private const string IMG_FOLDERTOOL = "folder_tool";
        private const string IMG_QUERY = "query";

       
        private const string DEFAULT_FILE = "C:\\Program Files (x86)\\K2 blackpearl\\ServiceBroker\\NAB.K2.SharePointSearch.conf";

        private bool _Saved = true;
        private string _CurrentFile = null;

        private ConfigurationStore _Store = new ConfigurationStore(true);

        private string _SelectedFolderId = null;



        public frmMain()
        {
            InitializeComponent();

            //Settings
            _CurrentFile = Properties.Settings.Default.LastConfigFile;
            if (string.IsNullOrEmpty(_CurrentFile))
            {
                _CurrentFile = DEFAULT_FILE;
            }

            //If the default file exists, then load it
            if(File.Exists(_CurrentFile))
            {
                _Load(_CurrentFile);
            }
            else
            {
                _LoadDisplay();

            }

        }



       

        #region Form Load


        private void _LoadDisplay()
        {
            //Main Labels
            labelName.Text = _Store.StoreName;
            labelVersion.Text = _Store.StoreVersion;

            //Clear the Tree
            treeFolders.Nodes.Clear();

            TreeNode mn = new TreeNode("Configuration");
            mn.ImageKey = IMG_CONFIG;
            mn.SelectedImageKey = IMG_CONFIG;
            mn.ToolTipText = "All configuration settings";
            treeFolders.Nodes.Add(mn);

            foreach (var f in _Store.Folders)
            {

                TreeNode n = new TreeNode();
                _FolderToTreeNode(f, n);

                mn.Nodes.Add(n);

            }

            //Select the first node
            treeFolders.SelectedNode = treeFolders.Nodes[0];
            treeFolders.Nodes[0].Expand();


            //Status Bar
            labelFilename.Text = _CurrentFile;

        }

        private void _FolderToTreeNode(StoreFolder f, TreeNode n)
        {
            n.Text = f.DisplayName;
            n.ImageKey = IMG_FOLDER;
            n.SelectedImageKey = IMG_FOLDER;
            n.ToolTipText = f.Description;
            n.Tag = f.FolderId;
        }


        private void _LoadQueries(string folderId)
        {
            List<QueryDef> l;

            if(folderId == null)
            {
                l = _Store.Queries;
            }
            else {

               l = _Store.GetQueryByFolder(folderId);
            
            }
            

            listQueries.Items.Clear();

            foreach (var q in l)
            {
                ListViewItem i = new ListViewItem();
                _QueryToListViewItem(q, i);
                listQueries.Items.Add(i);

            }


        }


        private void _QueryToListViewItem(QueryDef q, ListViewItem i)
        {
            i.Name = q.QueryId;
            i.Tag = q.QueryId;
            i.ImageKey = IMG_QUERY;

            i.SubItems[0].Text = q.QueryId;

            //Make sure there are 5 properties
            while(i.SubItems.Count < 5)
            {
                i.SubItems.Add(string.Empty);
            }
            
            i.SubItems[1].Text = q.DisplayName;
            i.SubItems[2].Text = q.Description;
            i.SubItems[3].Text = q.Columns.Count.ToString();
            i.SubItems[4].Text = q.Parameters.Count.ToString();

        }


        #endregion


        #region Load and Save

        private void _Save(string filename)
        {
            try
            {
                ConfigurationLoader.SaveStoreFromFile(_Store, filename);

            }catch(Exception e)
            {
                MessageBox.Show(string.Format("Unable to save: {0} - {1}", e.GetType().Name, e.Message));
                return;
            }
            
            _CurrentFile = filename;
            _Saved = true;
        }

        private void _Load(string filename)
        {
            try
            {
                ConfigurationStore qs = ConfigurationLoader.LoadStoreFromFile(filename);
                _Store = qs;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Unable to load: {0} - {1}", e.GetType().Name, e.Message));
                return;
            }

            _CurrentFile = filename;

            _Saved = true;

            _LoadDisplay();



        }

        private void buttonNewDocument_Click(object sender, EventArgs e)
        {

            //Check for changes
            if (!_SaveCheck())
                return;


            _Store = new ConfigurationStore(true);

            _CurrentFile = DEFAULT_FILE;

            _Saved = true;

            _LoadDisplay();


        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            //Check for changes
            if (!_SaveCheck())
                return;

            if(dialogOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _Load(dialogOpen.FileName);
            }
            

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            dialogSave.FileName = _CurrentFile;
            if(dialogSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _Save(dialogSave.FileName);
            }
            
        }


        private bool _SaveCheck()
        {
            if(!_Saved)
            {
                //Ask if they want to abandon changes
                return MessageBox.Show("The current configuration file is not saved.  Are you sure you want to continue and loose changes?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes;

            }else{
                return true;
            }

        }

        private void buttonConfigSettings_Click(object sender, EventArgs e)
        {

            if(frmProperties.EditConfig(_Store, this))
            {
                //Main Labels
                labelName.Text = _Store.StoreName;
                labelVersion.Text = _Store.StoreVersion;

                _Saved = false;

            }

        }

        #endregion

        #region Form General Events

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!_SaveCheck())
            {
                if(e.CloseReason != CloseReason.UserClosing)
                {
                                                          
                    //Save to temp location - just in case this was an unexpected shutdown
                    ConfigurationLoader.LoadStoreFromFile(Path.GetTempPath() + "NAB.K2.SharePointSearch.ShutdownCopy.conf");

                }

                e.Cancel = true;

            }

            Properties.Settings.Default.LastConfigFile = _CurrentFile;
            Properties.Settings.Default.Save();


        }

        #endregion
        
        #region Folder Actions

        private void buttonNewFolder_Click(object sender, EventArgs e)
        {

            StoreFolder f;

            if (frmFolder.NewFolder(_Store, out f, this))
            {
                _Store.Folders.Add(f);

                TreeNode n = new TreeNode();
                _FolderToTreeNode(f, n);

                treeFolders.Nodes[0].Nodes.Add(n);

                if (!treeFolders.Nodes[0].IsExpanded)
                {
                    treeFolders.Nodes[0].Expand();
                }

                _Saved = false;
            }




        }

        private void btnEditFolder_Click(object sender, EventArgs e)
        {

            TreeNode n = treeFolders.SelectedNode;

            //Get by id
            StoreFolder f = _Store.GetFolderById((string)n.Tag);

            if (frmFolder.EditFolder(_Store, f, this))
            {
                _FolderToTreeNode(f, n);

                _Saved = false;
            }
        }

        private void buttonDelFolder_Click(object sender, EventArgs e)
        {

            TreeNode n = treeFolders.SelectedNode;

            //Get by id
            StoreFolder f = _Store.GetFolderById((string)n.Tag);

            if(f != null)
            {
                List<QueryDef> l = _Store.GetQueryByFolder(f.FolderId);

                if(l.Count > 0)
                {
                    if(MessageBox.Show("Deleting this folder will also delete {0} queries.  Are you sure you want to do this?", "Delete Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Removes all for that folder
                        _Store.Queries.RemoveAll(q => q.FolderId == f.FolderId);

                    }
                    else
                    {
                        return;
                    }

                }

                //Remove the folder
                _Store.Folders.Remove(f);
                treeFolders.Nodes.Remove(n);
                
                //Update saves flag
                _Saved = false;

                //Select the root note
                treeFolders.SelectedNode = treeFolders.Nodes[0];


            }


        }

        private void treeFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Nothing yet
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node != null)
            {

                _SelectedFolderId = (string)e.Node.Tag;

                bool isFolder = !string.IsNullOrEmpty(_SelectedFolderId);

                buttonEditFolder.Enabled = isFolder;
                buttonDelFolder.Enabled = isFolder;

                if (isFolder)
                {
                    _LoadQueries(_SelectedFolderId);
                }
                else
                {
                    _LoadQueries(null);

                }

            }
            else
            {
                _SelectedFolderId = null;
            }


        }




        #endregion


        #region Query Actions


        private void buttonNewQuery_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(_SelectedFolderId))
            {
                MessageBox.Show("Please create or select a folder prior to creating a new query", "Select or Create Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            QueryDef q;
            if (frmQuery.NewQuery(_Store, _SelectedFolderId, out q, this))
            {
                _Store.Queries.Add(q);

                ListViewItem i = new ListViewItem();
                _QueryToListViewItem(q, i);

                listQueries.Items.Add(i);
                
                _Saved = false;
            }

        }

        private void buttonEditQuery_Click(object sender, EventArgs e)
        {
            if (listQueries.SelectedItems.Count == 0)
                return;


            ListViewItem i = listQueries.SelectedItems[0];

            //Get by id
            QueryDef q = _Store.GetQueryById((string)i.Tag);

            if (frmQuery.EditQuery(_Store, q, this))
            {
                _QueryToListViewItem(q, i);
                _Saved = false;
            }
        }

        private void buttonDeleteQuery_Click(object sender, EventArgs e)
        {
            if (listQueries.SelectedItems.Count == 0)
                return;


            ListViewItem i = listQueries.SelectedItems[0];

            if(MessageBox.Show("Are you sure you want to delete this query?", "Delete Query", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //Get by id
            QueryDef q = _Store.GetQueryById((string)i.Tag);

            //Remove from store
            _Store.Queries.Remove(q);


            //Remove from list view
            listQueries.Items.Remove(i);
        }

        private void listQueries_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            //Just like edit button
            buttonEditQuery_Click(sender, e);


        }

        /// <summary>
        /// Duplicate currently selected query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopyQuery_Click(object sender, EventArgs e)
        {
            
            if (listQueries.SelectedItems.Count == 0)
                return;


            ListViewItem i = listQueries.SelectedItems[0];

            //Get by id
            QueryDef q = _Store.GetQueryById((string)i.Tag);

            //Create Deep Clone
            q = (QueryDef)q.Clone();
                        
            //Add copy to the end
            q.QueryId += " (copy)";
            q.DisplayName += " (copy)";
            while (_Store.GetQueryById(q.QueryId) != null)
            {
                //This query already exists so
                //Just add "+" to make unique
                q.QueryId += "+";

                if(q.QueryId.Length > 60)
                {
                    MessageBox.Show("Query IDs must be less than 60 characters, unable to make copy.");
                    return;
                }
            }
                        
            //Add to the store
            _Store.Queries.Add(q);

            //Add to list view
            i = new ListViewItem();
            _QueryToListViewItem(q, i);
            listQueries.Items.Add(i);

            _Saved = false;

        }

        #endregion


        #region Other Toolbar Buttons

        private void buttonConnect_Click(object sender, EventArgs e)
        {

            DesignConnection.Configure(this);

        }       




        #endregion







    }
}
