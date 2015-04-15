// --------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Repositories.Interfaces
{
    using DBDeployment.Models.Users;
    using Fanex.Data.Repository;

    /// <summary>
    /// IUserRepository interface.
    /// </summary>
    public interface IUserRepository
    {
        UserModel GetUserByDomainUserName(ICriteria criteria);
    }
}
