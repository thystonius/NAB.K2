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
*   Namespace: NAB.K2.SharePointSearchEditor.Utility
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAB.K2.SharePointSearchEditor.Utility
{
    static class FormUtility
    {

        /// <summary>
        /// Centers a "child" form on top of a "parent" form.  There does not need to be any actual relationship between the forms, nor does either need to be modal
        /// </summary>
        /// <param name="childForm">Form to move</param>
        /// <param name="parentForm">Form to use for reference location</param>
        public static void CenterFormOnForm(Form childForm, Form parentForm)
        {
            //Set the location of the child form
            childForm.Location = new System.Drawing.Point(parentForm.Location.X + ((parentForm.Size.Width - childForm.Size.Width) / 2), parentForm.Location.Y + ((parentForm.Size.Height - childForm.Size.Height) / 2));

        }

    }
}
