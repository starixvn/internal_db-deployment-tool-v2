namespace DBDeployment.Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// HomeController class.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
