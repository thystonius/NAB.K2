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

using NAB.K2.SharePointSearchEditor.Utility;

namespace NAB.K2.SharePointSearchEditor
{
    public partial class frmConnection : Form
    {

        DesignConnection _conn;

        
        public frmConnection()
        {
            InitializeComponent();
        }


        public static DesignConnection ConfigureConnection(DesignConnection conn, Form parent)
        {
            
            frmConnection f = new frmConnection();

            if (conn == null)
            {
                f._conn = new DesignConnection();
                f._conn.SharePointUrl = Properties.Settings.Default.LastSharePointUrl;
            }
            else
            {
                f._conn = conn;
            }
            
            f.txtURL.Text = f._conn.SharePointUrl;
            f.txtRows.Text = f._conn.MaxRows.ToString();
            f.radioCurrent.Checked = f._conn.UseDefaultCredentials;
            f.radioOther.Checked = !f.radioCurrent.Checked;

            FormUtility.CenterFormOnForm(f, parent);

            f.ShowDialog();

            return f._conn;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            _conn.SharePointUrl = txtURL.Text;
            _conn.MaxRows = Convert.ToInt32(txtRows.Text);

            _conn.UseDefaultCredentials = radioCurrent.Checked;

            if (!_conn.UseDefaultCredentials)
            {
                //Create a credential object to store the credentials to use
                _conn.Credentials = new System.Net.NetworkCredential(txtUsername.Text, txtPassword.Text);
            }

            //Remember this just to make life easier (no real harm in this setting)
            Properties.Settings.Default.LastSharePointUrl = _conn.SharePointUrl;
            Properties.Settings.Default.Save();
        }

        private void radioOther_CheckedChanged(object sender, EventArgs e)
        {
            //Only enable if other
            groupCustom.Enabled = radioOther.Checked;

        }


    }
}
