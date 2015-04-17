namespace DBDeployment.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// RouteConfig class.
    /// </summary>
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Package", action = "CreatePackage", id = UrlParameter.Optional });
        }
    }
}