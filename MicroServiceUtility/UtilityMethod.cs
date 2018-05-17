using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceUtility
{
    public class UtilityMethod
    {
        /// <summary>
        /// 注册应用
        /// </summary>
        /// <param name="inputApplicationName"></param>
        /// <param name="inputServiceApi"></param>
        /// <param name="inputHost"></param>
        /// <param name="inputPort"></param>
        public static void RegiestedService(string inputApplicationName,string inputServiceApi,string inputHost,int inputPort)
        {
            HttpClient tempClient = new HttpClient();

            //配置Uri
            string tempUri = inputServiceApi + "?" + "serviceName=" + inputApplicationName 
                + "&" + "hostName=" + inputHost + "&" + "port=" + inputPort.ToString();

            var tempResult = tempClient.GetAsync(tempUri).Result;
        }
    }
}
