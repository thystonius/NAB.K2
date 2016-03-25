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
using System.Text;
using System.Threading.Tasks;
using System.Security;

using System.Windows.Forms;

using NAB.K2.SharePointSearch.Runtime;
using NAB.K2.SharePointSearch.Configuration;
using System.Net;

namespace NAB.K2.SharePointSearchEditor
{
    /// <summary>
    /// Singleton class used during desing to simulate a runtime connection
    /// </summary>
    public class DesignConnection : IRuntimeConnection
    {
        
        #region Static Singleton for Design

        private static DesignConnection _connection = null;

        public static DesignConnection GetGlobalConnection()
        {
            
            if(_connection == null)
            {
                //WARNING - this is not thread safe as no lock on creating of this singleton
                //But this is in a GUI app so shouldn't matter  (we need to do this on-demand, so we are not asking for this on every startup, only when they run a test)

                _connection = frmConnection.ConfigureConnection(null, Program.MainForm);

            }

            return _connection;

        }


        public static void Configure(Form parent)
        {

            _connection = frmConnection.ConfigureConnection(_connection, parent);

        }

        #endregion


        public Guid ServiceId { get; set; }
        public string SharePointUrl { get; set; }
        public int MaxRows{ get; set; }
        public int MaxTimeout { get; set; }
        public bool AllowCaching { get {return false;} set { /* Do nothing */ } }

        public bool UseDefaultCredentials { get; set; }
        public NetworkCredential Credentials { get; set; }


        /// <summary>
        /// ctor
        /// </summary>
        public DesignConnection()
        {
            //Defaults for design time
            ServiceId = Guid.NewGuid();
            MaxRows = 100;
            MaxTimeout = 30;            

            //Default to using default
            UseDefaultCredentials = true;
        }
        

        public void AuthenticateContext(Microsoft.SharePoint.Client.ClientContext context)
        {
            //During design this should at least prompt the user for the credentials to use
            
            if(UseDefaultCredentials)
            {
                context.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else
            {
                context.Credentials = Credentials;
            }
            
        }


        /// <summary>
        /// Designe time version of this method WILL REBUILD QueryRuntime FOR EACH CALL.
        /// This is optomized for design-time
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public QueryRuntime GetQueryRuntime(QueryDef query)
        {

            return new QueryRuntime(query, this);

        }

        


    }
}
