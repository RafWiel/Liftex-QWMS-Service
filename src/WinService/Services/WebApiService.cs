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

namespace WinService.Services
{
    public class WebApiService : IBaseService
    {       
        private HttpSelfHostServer _server;

        private IBarcodesService _barcodesService;
        private IOrdersService _ordersService;
        private IProductsService _productsService;
        private IReservationsService _reservationsService;

        public WebApiService(
            IBarcodesService barcodesService,
            IOrdersService ordersService,
            IProductsService productsService,
            IReservationsService reservationsService)
        {
            _barcodesService = barcodesService;
            _ordersService = ordersService;
            _productsService = productsService;
            _reservationsService = reservationsService;
        }
        
        public void Start()
        {
            var config = new HttpSelfHostConfiguration(WebApiConfiguration.Address);

            config.DependencyResolver = new OverriddenWebApiDependencyResolver(config.DependencyResolver)
                .Add(typeof(BarcodesController), () => new BarcodesController(_barcodesService))
                .Add(typeof(OrdersController), () => new OrdersController(_ordersService))            
                .Add(typeof(ProductsController), () => new ProductsController(_productsService))
                .Add(typeof(ReservationsController), () => new ReservationsController(_reservationsService));

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
