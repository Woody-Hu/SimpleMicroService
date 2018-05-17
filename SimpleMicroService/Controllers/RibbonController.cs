using SimpleMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Unity.Attributes;

namespace SimpleMicroService.Controllers
{
    /// <summary>
    /// Api服务
    /// </summary>
    public class RibbonController : ApiController
    {
        /// <summary>
        /// 服务信息单例
        /// </summary>
        [Dependency]
        public ServiceInfo UseServices { set; get; }

        /// <summary>
        /// 查询字符串中服务名称
        /// </summary>
        private const string m_strServiceName = "serviceName";

        /// <summary>
        /// 查询字符串中api参数名
        /// </summary>
        private const string m_strUseApi = "useApi";

        /// <summary>
        /// 调度服务
        /// </summary>
        /// <param name="useServiceName"></param>
        /// <returns></returns>
        [HttpGet]
        public object RibbonMethod(string useServiceName)
        {
            var tempRequest = HttpContext.Current.Request;

            //切分服务参数
            var splitedValues = useServiceName.Split('/');

            //获取服务列表
            var useClients = UseServices.GetServices(splitedValues[0].ToLower());

            //负载均衡随机算法
            var useClient = GetRandomClient(useClients);

            if (null == useClient)
            {
                return "None Service";
            }
            else
            {
                //制作Api
                StringBuilder useApi = new StringBuilder();

                for (int i = 1; i < splitedValues.Length; i++)
                {
                    useApi.Append(splitedValues[i] + "/");
                }

                string useApiPath = useApi.ToString().TrimEnd('/');

                //发起远程代理
                return TransforRequest(useApiPath, useClient,false);
            }
        }

        /// <summary>
        /// 调用服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="useApi"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetService(string serviceName,string useApi)
        {
            serviceName = serviceName.Trim();

            var useClients = UseServices.GetServices(serviceName);

            //负载均衡随机算法
            var useClient = GetRandomClient(useClients);

            if (null == useClient)
            {
                return "None Service";
            }
            else
            {
                return TransforRequest(useApi, useClient);
                 
            }
        }

        /// <summary>
        /// 派发请求
        /// </summary>
        /// <param name="useApi"></param>
        /// <param name="useClient"></param>
        /// <param name="ifContinueQueryString"></param>
        /// <returns></returns>
        private object TransforRequest(string useApi, ClinentInfo useClient,bool ifContinueQueryString = true)
        {
            //请求跳转
            StringBuilder usePath = new StringBuilder();

            usePath.Append(useClient.UseHostName + ":" + useClient.UsePort + "/" + useApi);

            var useQueryStrings = HttpContext.Current.Request.QueryString;

            bool tag = true;

            foreach (var oneKey in useQueryStrings.AllKeys)
            {
                //查询字符串跳过判断
                if (ifContinueQueryString && (oneKey == m_strServiceName || m_strServiceName == m_strUseApi))
                {
                    continue;
                }

                //附加参数
                if (tag)
                {
                    tag = false;
                    usePath.Append("?");
                }

                usePath.Append(oneKey + "=" + useQueryStrings[oneKey] + "&");
            }

            //使用的跳转地址
            var tempPath = usePath.ToString().TrimEnd('&');

            //发起远程代理
            HttpClient tempUseClient = new HttpClient();

            var tempResult = tempUseClient.GetAsync(tempPath).Result;

            return tempResult;
        }

        /// <summary>
        /// 获得一个随机的Client
        /// </summary>
        /// <param name="lstClientInfo"></param>
        /// <returns></returns>
        private ClinentInfo GetRandomClient(List<ClinentInfo> lstClientInfo)
        {
            if (null == lstClientInfo || 0 == lstClientInfo.Count)
            {
                return null;
            }
            else
            {
                int tempCount = lstClientInfo.Count;

                Random tempRandow = new Random();

                int tempIndex = tempRandow.Next(0, tempCount);

                return lstClientInfo[tempIndex];
            }
        }

    }
}
