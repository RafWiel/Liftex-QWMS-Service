using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using System.Web.Http.SelfHost;
using WinService.Controllers;
using WinService.Helpers;
using WinService.AppStart;

namespace WinService.Services
{
    public class WebApiService
    {       
        private HttpSelfHostServer _server;

        public OrdersService OrdersService { get; set; }
        public ProductsService ProductsService { get; set; }

        public void Start()
        {
            var config = new HttpSelfHostConfiguration(WebApiConfiguration.Address);

            config.DependencyResolver = new OverriddenWebApiDependencyResolver(config.DependencyResolver)
                .Add(typeof(OrdersController), () => new OrdersController(OrdersService))            
                .Add(typeof(ProductsController), () => new ProductsController(ProductsService));

            WebApiConfiguration.Register(config);

            _server = new HttpSelfHostServer(config);
            _server.OpenAsync().Wait();
        }

        public void Stop()
        {
            _server.CloseAsync();
        }       
    }
}
