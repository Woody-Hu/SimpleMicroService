using SimpleMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity.Attributes;

namespace SimpleMicroService.Controllers
{
    public class ServiceController : ApiController
    {
        /// <summary>
        /// 服务信息单例
        /// </summary>
        [Dependency]
        public ServiceInfo UseServices { set; get; }

        public string Get()
        {
            return UseServices.ToString();
        }

        [HttpGet]
        public bool AddService(string serviceName,string hostName,int port)
        {
            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(hostName) || port <= 0 )
            {
                return false;
            }
            ClinentInfo tempClientInfo = new ClinentInfo(serviceName, hostName, port);
            UseServices.AddService(tempClientInfo);
            return true;
        }


    }
}
