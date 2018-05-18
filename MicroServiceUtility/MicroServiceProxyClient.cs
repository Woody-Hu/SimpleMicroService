using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceUtility
{
    /// <summary>
    /// 代理Api客户端
    /// </summary>
    public class MicroServiceProxyClient
    {
        /// <summary>
        /// 使用的动态Api服务
        /// </summary>
        private string m_useServiceDynamicApi;

        /// <summary>
        /// 构造代理客户端
        /// </summary>
        /// <param name="inputDynamicApi"></param>
        public MicroServiceProxyClient(string inputDynamicApi)
        {
            m_useServiceDynamicApi = inputDynamicApi.TrimEnd('/');
        }

        /// <summary>
        /// 使用远程Api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputApplicationName"></param>
        /// <param name="inputUseApi"></param>
        /// <param name="inputParameters"></param>
        /// <param name="resultValue"></param>
        /// <returns></returns>
        public bool TryGetResult<T>(string inputApplicationName,string inputUseApi,List<KeyValuePair<string,string>> inputParameters ,out T resultValue )
        {

            resultValue = default(T);

            try
            {
                //数据预处理
                inputApplicationName = inputApplicationName.Trim('/');
                inputUseApi = inputUseApi.Trim('/');

                //制作远程Api
                StringBuilder tempStringBuilder = new StringBuilder();
                tempStringBuilder.Append((m_useServiceDynamicApi + "/" + inputApplicationName + "/" + inputUseApi + "/"));

                if (inputParameters.Count > 0)
                {
                    tempStringBuilder.Append("?");

                    int parameterCount = inputParameters.Count;

                    for (int i = 0; i < parameterCount; i++)
                    {
                        tempStringBuilder.Append(string.Format("{0}={1}", inputParameters[i].Key, inputParameters[i].Value));

                        if (i != parameterCount - 1)
                        {
                            tempStringBuilder.Append("&");
                        }
                    }
                }


                //制作远程客户端
                HttpClient useClient = new HttpClient();


                //发起远程Api并转换
                resultValue = useClient.GetAsync(tempStringBuilder.ToString()).Result.Content.ReadAsAsync<T>().Result;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
           


        
        }
    }
}
