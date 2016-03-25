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
using NAB.K2.SharePointSearchEditor.Utility;

namespace NAB.K2.SharePointSearchEditor
{
    public partial class frmProperties : Form
    {
        ConfigurationStore _store;


        public frmProperties()
        {
            InitializeComponent();
        }

        private void _LoadForm()
        {

            txtName.Text = _store.StoreName;
            txtVersion.Text = _store.StoreVersion;
            txtBuild.Text = _store.StoreBuild.ToString();
        }


        public static bool EditConfig(ConfigurationStore store, Form parent)
        {

            frmProperties f = new frmProperties();
            f._store = store;

            f._LoadForm();

            FormUtility.CenterFormOnForm(f, parent);

            return f.ShowDialog() == DialogResult.OK;
            
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _store.StoreName = txtName.Text;
            _store.StoreVersion = txtVersion.Text;


        }

    }
}
