// --------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Nexcel Solutions Vietnam">
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
    using DBDeployment.Repositories.User;
    using Fanex.Common.Helper;

    /// <summary>
    /// UserService class.
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService()
            : this(null)
        {
        }

        public UserService(IUserRepository repository)
        {
            _repository = repository ?? new UserRepository();
        }

        /// <summary>
        /// Gets the name of the user by domain user.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The user model.</returns>
        public UserModel GetUserByDomainUserName(GetUserByDomainUserNameCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            UserModel user = null;

            if (criteria.IsValid())
            {
                user = _repository.GetUserByDomainUserName(criteria);
            }

            return user;
        }
    }
}
