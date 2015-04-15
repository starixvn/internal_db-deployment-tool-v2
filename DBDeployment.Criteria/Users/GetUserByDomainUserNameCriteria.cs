// --------------------------------------------------------------------------
// <copyright file="GetUserByDomainUserNameCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Criteria.Users
{
    using Fanex.Data.Repository;

    /// <summary>
    /// GetUserByDomainUserNameCriteria class.
    /// </summary>
    public class GetUserByDomainUserNameCriteria : CriteriaBase
    {
        public string DomainUserName { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(DomainUserName);
        }

        public override string GetSettingKey()
        {
            return "spUser_GetUserByDomainUserName";
        }
    }
}
