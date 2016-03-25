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

using NAB.K2.SharePointSearch;
using NAB.K2.SharePointSearch.Configuration;
using NAB.K2.SharePointSearchEditor.Utility;

namespace NAB.K2.SharePointSearchEditor
{
    public partial class frmFolder : Form
    {

        private bool _IsNew = false;
        private ConfigurationStore _Store;
        private StoreFolder _Folder = new StoreFolder();


        public frmFolder()
        {
            InitializeComponent();
        }

        private void frmFolder_Load(object sender, EventArgs e)
        {

        }


        private void _LoadForm()
        {

            txtFolderId.Text = _Folder.FolderId;
            txtName.Text = _Folder.DisplayName;
            txtDescription.Text = _Folder.Description;

        }

        private void _GetValues()
        {

            _Folder.FolderId = txtFolderId.Text;
            _Folder.DisplayName = txtName.Text;
            _Folder.Description = txtDescription.Text;

        }


        #region Static Accessors
        public static bool NewFolder(ConfigurationStore store, out StoreFolder newFolder, Form parent)
        {

            frmFolder f = new frmFolder();
            f._Store = store;
            f._IsNew = true;

            //Center the form
            FormUtility.CenterFormOnForm(f, parent);

            if (f.ShowDialog() == DialogResult.OK)
            {
                newFolder = f._Folder;
                return true;
            }
            else
            {
                newFolder = null;
                return false;
            }

        }

        public static bool EditFolder(ConfigurationStore store, StoreFolder folder, Form parent)
        {
            frmFolder f = new frmFolder();

            f._Store = store;
            f._Folder = folder;
            f._LoadForm();
            f.txtFolderId.ReadOnly = true;

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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (txtFolderId.Text.Length == 0)
            {
                MessageBox.Show("Must specify a folder id");
                txtFolderId.Focus();
                return;
            }

            if (_IsNew)
            {
                if(_Store.GetFolderById(txtFolderId.Text) != null)
                {
                    MessageBox.Show("Folder Id already exists.  Must specify a unique folder id");
                    txtFolderId.Focus();
                    return;
                }
            
            }
            



            if(txtName.Text.Length == 0)
            {
                MessageBox.Show("Must specify a display name.");
                txtName.Focus();
                return;
            }

            
            _GetValues();


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }



    }
}
