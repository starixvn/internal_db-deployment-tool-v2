// --------------------------------------------------------------------------
// <copyright file="SessionManager.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Core.Utilities
{
    using System.Web;
    using DBDeployment.Models.Users;

    /// <summary>
    /// SessionManager class.
    /// </summary>
    public static class SessionManager
    {
        private const string CurrentUserSessionKey = "DBDeployment.Sessions.CurrentUser";

        public static UserModel CurrentUser
        {
            get
            {
                return HttpContext.Current.Session[CurrentUserSessionKey] as UserModel;
            }

            set
            {
                HttpContext.Current.Session[CurrentUserSessionKey] = value;
            }
        }
    }
}
