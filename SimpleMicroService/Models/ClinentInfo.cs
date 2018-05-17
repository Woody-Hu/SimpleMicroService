using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMicroService.Models
{
    /// <summary>
    /// 服务客户端信息
    /// </summary>
    public class ClinentInfo
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { private set; get; }

        /// <summary>
        /// 服务主机地址
        /// </summary>
        public string UseHostName { private set; get; }

        /// <summary>
        /// 服务端口号
        /// </summary>
        public int UsePort {private set; get; }

        public ClinentInfo(string inputServiceName,string inputHostName,int inputPort)
        {
            ServiceName = inputServiceName.ToLower();
            UseHostName = inputHostName.TrimEnd('/');
            UsePort = inputPort;
        }

    }
}