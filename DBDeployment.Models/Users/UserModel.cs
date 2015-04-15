// --------------------------------------------------------------------------
// <copyright file="UserModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Models.Users
{
    /// <summary>
    /// UserModel class.
    /// </summary>
    public class UserModel
    {
        public int UserId { get; set; }

        public string DomainUserName { get; set; }
    }
}
