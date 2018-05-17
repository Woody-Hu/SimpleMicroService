using SimpleMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Unity.Attributes;
using UnityUtility;

namespace SimpleMicroService.Utility
{
    [Compent(RegistByClass = true,Singleton = true)]
    public class UseRouteConstraint : IRouteConstraint
    {
        [Dependency]
        public ServiceInfo UseServiceInfo { set; get; }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //获取服务参数
            string inputserviceAndApi = values[parameterName].ToString();

            //切分服务参数
            var splitedValues = inputserviceAndApi.Split('/');

            return UseServiceInfo.IfContainsService(splitedValues[0].ToLower());
        }
    }
}