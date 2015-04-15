// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.15">Created</change>
// </history>

namespace DBDeployment.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using DBDeployment.Core.Utilities;
    using DBDeployment.Criteria.Users;
    using DBDeployment.Models.Users;
    using DBDeployment.Services.Implement;
    using DBDeployment.Services.Interfaces;

    /// <summary>
    /// BaseController class.
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        private readonly IUserService _userService;        

        public BaseController()
            : this(null)
        {
        }

        public BaseController(IUserService userService)
        {
            _userService = userService ?? new UserService();
        }

        public UserModel CurrentUser
        {
            get
            {
                return SessionManager.CurrentUser;
            }
        }

        /// <summary>
        /// Called when authorization occurs.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (SessionManager.CurrentUser == null)
                {
                    var criteria = new GetUserByDomainUserNameCriteria
                    {
                        DomainUserName = filterContext.HttpContext.User.Identity.Name
                    };

                    UserModel user = _userService.GetUserByDomainUserName(criteria);
                    SessionManager.CurrentUser = user;
                }
            }
        }
    }
}
