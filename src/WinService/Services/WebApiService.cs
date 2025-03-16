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
using WinService.Interfaces;
using System.Reflection;

namespace WinService.Services
{
    public class WebApiService : BaseService
    {       
        private HttpSelfHostServer _server;
        private string _address;

        private IBarcodesService _barcodesService;
        private IOrdersService _ordersService;
        private IProductsService _productsService;
        private IReservationsService _reservationsService;        

        public WebApiService(
            string address,
            IBarcodesService barcodesService,
            IOrdersService ordersService,
            IProductsService productsService,
            IReservationsService reservationsService)
        {
            _address = address;
            _barcodesService = barcodesService;
            _ordersService = ordersService;
            _productsService = productsService;
            _reservationsService = reservationsService;
        }
        
        public override void Start()
        {
            InvokeLogEvent($"Uruchamianie web serwera - {_address}");

            var config = new HttpSelfHostConfiguration(_address);

            config.DependencyResolver = new OverriddenWebApiDependencyResolver(config.DependencyResolver)
                .Add(typeof(BarcodesController), () => new BarcodesController(_barcodesService))
                .Add(typeof(OrdersController), () => new OrdersController(_ordersService))            
                .Add(typeof(ProductsController), () => new ProductsController(_productsService))
                .Add(typeof(ReservationsController), () => new ReservationsController(_reservationsService));

            WebApiConfiguration.Register(config);

            _server = new HttpSelfHostServer(config);
            _server.OpenAsync().Wait();            
        }

        public override void Stop()
        {
            _server.CloseAsync();
        }       
    }
}
