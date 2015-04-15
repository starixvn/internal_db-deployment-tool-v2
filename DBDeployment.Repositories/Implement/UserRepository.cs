// --------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Repositories.Implement
{
    using DBDeployment.Models.Users;
    using DBDeployment.Repositories.Interfaces;
    using Fanex.Data;
    using Fanex.Data.Repository;

    /// <summary>
    /// UserRepository class.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public UserModel GetUserByDomainUserName(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<UserModel>(criteria.GetSpParams());
            }
        }
    }
}
