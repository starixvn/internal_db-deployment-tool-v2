// --------------------------------------------------------------------------
// <copyright file="PackageController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.17">Created</change>
// </history>

namespace DBDeployment.Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// PackageController class.
    /// </summary>
    public class PackageController : BaseController
    {
        public ActionResult CreatePackage()
        {
            return View();
        }
    }
}
