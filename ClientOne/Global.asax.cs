using MicroServiceUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ClientOne
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //注册应用
            UtilityMethod.RegiestedService("test", "http://localhost:2271/api/Service", "http://localhost", 16704);
        }
    }
}
