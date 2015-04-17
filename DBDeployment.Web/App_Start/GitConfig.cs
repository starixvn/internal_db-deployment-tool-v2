// --------------------------------------------------------------------------
// <copyright file="GitConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.16">Created</change>
// </history>

namespace DBDeployment.Web
{
    using DBDeployment.Core.Utilities;
    using NGit.Transport;

    /// <summary>
    /// GitConfig class.
    /// </summary>
    public static class GitConfig
    {
        public static void LoginGitAccount()
        {
            var gitUserPassword = Fanex.Common.Cryptography.CfsDeCrypt(WebAppSettings.GitPassword);

            var credentials = new UsernamePasswordCredentialsProvider(WebAppSettings.GitUser, gitUserPassword);
            CredentialsProvider.SetDefault(credentials);
        }
    }
}