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

using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace NAB.K2.SharePointSearch
{
    /// <summary>
    /// Utility class with methods relating to ServiceObjects, their properties, and metadata.
    /// </summary>
    static class ServiceObjectHelper
    {

        /// <summary>
        /// Creates a new Property for the ServiceObject and adds it to the properties collection
        /// </summary>
        /// <param name="so">ServiceObject instance</param>
        /// <param name="id">Unique ID of the property</param>
        /// <param name="name">Display Name</param>
        /// <param name="description">Metadata description</param>
        /// <param name="soType">Standard Smart Object Type</param>
        /// <returns>Newly created property (it will be automatically added to the Properties collection)</returns>
        public static Property CreateProperty(ServiceObject so, string id, string name, string description, SoType soType)
        {
            Property prop;
            prop = so.Properties.Create(id);
            prop.MetaData.DisplayName = name;
            prop.MetaData.Description = description;
            prop.SoType = soType;

            //Force TypeName for DateTimes
            if (prop.SoType == SoType.DateTime)
            {
                prop.Type = "System.DateTime";
            }

            return prop;
        }

        /// <summary>
        /// Creates a method parameter and adds it ot the method's parameter collection
        /// </summary>
        /// <param name="so"></param>
        /// <param name="method"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="soType"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static MethodParameter CreateMethodParameter(ServiceObject so, Method method, string id, string name, string description, SoType soType, bool isRequired)
        {
            MethodParameter param;
                        
            param = method.MethodParameters.Create(id);
            param.MetaData.DisplayName = name;
            param.MetaData.Description = description;
            param.SoType = soType;
            param.IsRequired = isRequired;

            return param;
        }


        


    }
}
