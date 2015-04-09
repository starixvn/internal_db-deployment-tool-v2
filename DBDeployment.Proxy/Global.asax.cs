namespace DBDeployment.Proxy
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    using System.Web.Http;    

    /// <summary>
    /// WebApiApplication class.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}