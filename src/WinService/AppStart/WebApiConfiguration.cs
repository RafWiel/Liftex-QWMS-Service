using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using System.Web.Http.SelfHost;

namespace WinService.AppStart
{
    public static class WebApiConfiguration
    {
        public const string Address = "http://localhost:3001";

        public static void Register(HttpSelfHostConfiguration config)
        {            
            config.Routes.Add("Values", new HttpRoute("api/v1/values/{value}", new HttpRouteValueDictionary(new
            {
                controller = "Values",
                action = "Get"
            })));

            config.Routes.Add("Orders", new HttpRoute("api/v1/orders", new HttpRouteValueDictionary(new 
            { 
                controller = "Orders",
                action = "Get"
            })));
        }
    }
}
