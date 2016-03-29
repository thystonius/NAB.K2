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
using System.Threading.Tasks;
using System.Windows.Forms;

using NAB.K2.SharePointSearch.Runtime;

namespace NAB.K2.SharePointSearchEditor
{
    
    static class Program
    {
        internal static frmMain MainForm { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Start the main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new frmMain();

            Application.Run(MainForm);

        }
    }
}
