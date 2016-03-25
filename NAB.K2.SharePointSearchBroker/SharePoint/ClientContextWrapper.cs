#region Copyright (c) 2012-2014 Multi Parts Supply, Inc.
/**********************************************************************************
*   Copyright (C) 2012-2014 Multi Parts Supply, Inc.
*   All rights reserved.
*   Proprietary and confidential
*   
*   Project: MPS.K2.SharePointSearch
*   Namespace: MPS.K2.SharePointSearch.SharePoint
*   Written by: nathan.brown 
*   Created on: 2015-5-15 at 3:54 PM
**********************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.SharePoint.Client;

namespace MPS.K2.SharePointSearch.SharePoint
{
    public class ClientContextWrapper
    {
        

        /// <summary>
        /// Collection of Context Wrappers per "site" key
        /// </summary>
        private static Dictionary<string, ClientContextWrapper> _instances = new Dictionary<string, ClientContextWrapper>();
        private static NetworkCredential _defaultCredentials = null;


        private string _site = null;
        private ClientContext _context = null;
        private NetworkCredential _credentials = null;

        /// <summary>
        /// Singlton private constructor
        /// </summary>
        private ClientContextWrapper(string site)
        {
            _site = site;
            _context = new ClientContext(site);
        }

        public void SetCredentials(NetworkCredential credentials)
        {
            _credentials = credentials;
            _context.Credentials = _credentials;

        }

        /// <summary>
        /// Gets the client context
        /// </summary>
        public ClientContext Context { get { return _context; } }

        public void CloseContext()
        {
            _context.Dispose();
        }

               

        #region Static Access Methods

        public static void SetDefaultCredentials(NetworkCredential credentials)
        {
            _defaultCredentials = credentials;
        }

        public static ClientContextWrapper GetInstance(string site)
        {
            if (!_instances.ContainsKey(site.ToUpper()))
            {
                ClientContextWrapper instance = new ClientContextWrapper(site);

                if (_defaultCredentials == null)
                {
                    //Obtain default network credentials
                    _defaultCredentials = CredentialCache.DefaultNetworkCredentials;

                }

                instance.SetCredentials(_defaultCredentials);

                _instances.Add(site.ToUpper(), instance);

                return instance;
            }
            else
            {
                return _instances[site.ToUpper()];
            }

        }

        /// <summary>
        /// Get the current client context
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public static ClientContext GetContext(string site)
        {
            return GetInstance(site)._context;
        }

        /// <summary>
        /// Indicates if this site has an existing client already
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public static bool ContextExists(string site)
        {

            return _instances.ContainsKey(site.ToUpper());
            
        }


        /// <summary>
        /// Tests credentials agains an SP site
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="credentials"></param>
        /// <param name="responseString"></param>
        /// <returns></returns>
        public static bool TestCredentials(string siteUrl, NetworkCredential credentials, out string responseString)
        {
            ClientContext cc = new ClientContext(siteUrl);
            cc.Credentials = credentials;

            bool result = false;

            try
            {
                Web site = cc.Web;
                cc.Load(site);
                cc.ExecuteQuery();

                responseString = string.Format("Connected to: {0}", site.Title);
                result = true;
            }
            catch (Exception ex)
            {
                responseString = string.Format("Failed to connect: {0}", ex.Message);

            }
            finally
            {
                //Dispose of the client context
                cc.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Releases all client contexts
        /// </summary>
        public static void CloseAllContexts()
        {
            foreach (var c in _instances)
                c.Value.CloseContext();

            //Clear instances
            _instances.Clear();
        }


        #endregion






    }
}
