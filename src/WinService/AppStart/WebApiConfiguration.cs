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
            config.Routes.Add("Barcodes", new HttpRoute("api/v1/barcodes", new HttpRouteValueDictionary(new
            {
                controller = "Barcodes",
                action = "Get"
            })));

            config.Routes.Add("Orders", new HttpRoute("api/v1/orders", new HttpRouteValueDictionary(new 
            { 
                controller = "Orders",
                action = "Get"
            })));

            config.Routes.Add("OrdersTest", new HttpRoute("api/v1/orders/test", new HttpRouteValueDictionary(new
            {
                controller = "Orders",
                action = "Test"
            })));

            config.Routes.Add("Products", new HttpRoute("api/v1/products", new HttpRouteValueDictionary(new
            {
                controller = "Products",
                action = "Get"
            })));

            config.Routes.Add("Product", new HttpRoute("api/v1/product", new HttpRouteValueDictionary(new
            {
                controller = "Products",
                action = "GetSingle"
            })));

            config.Routes.Add("Reservations", new HttpRoute("api/v1/reservations", new HttpRouteValueDictionary(new
            {
                controller = "Reservations",
                action = "Get"
            })));

        }
    }
}


