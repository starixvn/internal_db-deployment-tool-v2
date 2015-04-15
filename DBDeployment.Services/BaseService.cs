// --------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Services
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.Data.Repository;

    /// <summary>
    /// Query Service.
    /// </summary>
    public class BaseService
    {
        private IDynamicRepository _dynamicRepository;

        public BaseService(IDynamicRepository dynamicRepository = null)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
        }

        /// <summary>
        /// Function to get list object from database.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="criteria">Criteria to query database.</param>
        /// <returns>List Object.</returns>
        public virtual IEnumerable<T> GetData<T>(ICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                return _dynamicRepository.Fetch<T>(criteria);
            }

            return new List<T>();
        }

        /// <summary>
        /// Function to get object from database.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="criteria">Criteria to query database.</param>
        /// <returns>The object.</returns>
        public virtual T GetDataEntity<T>(ICriteria criteria)
        {
            if (criteria.IsValid())
            {
                return _dynamicRepository.Get<T>(criteria);
            }

            return default(T);
        }
    }
}
