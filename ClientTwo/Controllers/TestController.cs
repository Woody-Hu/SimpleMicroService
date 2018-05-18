using MicroServiceUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientTwo.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {

            MicroServiceProxyClient tempClient = new MicroServiceProxyClient("http://localhost:2271/dynamicApi");

            string testValue;



            bool tag = tempClient.TryGetResult<string>("test", "api/Test", new List<KeyValuePair<string, string>>(), out testValue);


            return "BB" + testValue;
        }
    }
}
