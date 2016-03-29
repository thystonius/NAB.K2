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
*   Namespace: NAB.K2.SharePointSearch
*   Written by: nathan.brown 
**********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAB.K2.SharePointSearch
{

    public enum NABK2QueryEngines
    {

        SPListCalmEngine = 1,
        SPSearchEngine = 2


    }

    /// <summary>
    /// Duplicate of the K2SoTypes
    /// This is to allow the Designer to run completely outside of a K2 Environment.
    /// </summary>
    public enum NABK2SoType
    {
        Text = 0,
        Memo = 1,
        Number = 2,
        Decimal = 3,
        Autonumber = 4,
        YesNo = 5,
        DateTime = 6,
        Image = 7,
        File = 8,
        MultiValue = 9,
        Xml = 10,
        Default = 11,
        HyperLink = 12,
        Guid = 13,
        AutoGuid = 14,
        Date = 15,
        Time = 16,
    }


}
