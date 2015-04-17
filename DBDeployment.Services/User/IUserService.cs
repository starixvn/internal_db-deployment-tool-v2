// --------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Services.User
{
    using DBDeployment.Criteria.Users;
    using DBDeployment.Models.Users;

    /// <summary>
    /// IUserService interface.
    /// </summary>
    public interface IUserService
    {
        UserModel GetUserByDomainUserName(GetUserByDomainUserNameCriteria criteria);
    }
}
