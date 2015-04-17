// --------------------------------------------------------------------------
// <copyright file="WebAppSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.16">Created</change>
// </history>

namespace DBDeployment.Core.Utilities
{
    using System.Web.Configuration;

    /// <summary>
    /// WebAppSettings class.
    /// </summary>
    public static class WebAppSettings
    {
        private const string GitCommandFilePathKey = "GitCommandFilePath";
        private const string GitUserKey = "GitUser";
        private const string GitPasswordKey = "GitPassword";
        private const string GitUserNameKey = "GitUserName";
        private const string GitUserEmailKey = "GitUserEmail";

        public static string GitCommandFilePath
        {
            get
            {
                return WebConfigurationManager.AppSettings[GitCommandFilePathKey];
            }
        }

        public static string GitUser
        {
            get
            {
                return WebConfigurationManager.AppSettings[GitUserKey];
            }
        }

        public static string GitPassword
        {
            get
            {
                return WebConfigurationManager.AppSettings[GitPasswordKey];
            }
        }

        public static string GitUserName
        {
            get
            {
                return WebConfigurationManager.AppSettings[GitUserNameKey];
            }
        }

        public static string GitUserEmail
        {
            get
            {
                return WebConfigurationManager.AppSettings[GitUserEmailKey];
            }
        }
    }
}
