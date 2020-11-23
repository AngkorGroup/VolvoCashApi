using System.Web.Http;

namespace VolvoCash.DistributedServices.MainContext.Reports
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
