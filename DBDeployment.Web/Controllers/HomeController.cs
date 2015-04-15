namespace DBDeployment.Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// HomeController class.
    /// </summary>    
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
