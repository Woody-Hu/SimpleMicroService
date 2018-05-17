using SimpleMicroService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimpleMicroService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //配置动态Api路由
            config.Routes.MapHttpRoute(name: "DynamicApi", routeTemplate: "dynamicApi/{*useServiceName}",
                defaults: new { controller = "Ribbon", action = "RibbonMethod" },
                constraints:new { useServiceName = UnityUtility.UnityApplication.GetApplication().Reslove<UseRouteConstraint>() }
                );

            //默认webApi路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
