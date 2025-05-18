#nullable enable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinService.Configuration;
using WinService.Database;
using WinService.Interfaces;
using QWMS.Models.Orders;
using WinService.Models;
using WinService.DataTransferObjects;

namespace WinService.Services
{
    #if !MOCKUP

    public class OrdersService : BaseRequestsService, IOrdersService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();        

        public async Task<List<OrderListModel>?> Get(string? search, int? page)
        {
            try
            {
                InvokeLogEvent($"Wysyłanie listy zamówień");

                using (var db = new CdnDatabaseClient(DatabaseConfiguration))
                {
                    db.LogError += InvokeLogError;

                    return await db.GetOrders(search, page);                    
                }
            }
            catch (Exception ex)
            {
                InvokeLogError(ex.ToString());                
            }

            return null;

            //var models = new List<OrderModel>();

            ////na razie Task.Run, ale docelowo cmd.ExecuteReaderAsync
            //await Task.Run(() => {
            //    for (int i = 0; i < 1000; i++)
            //        models.Add(new OrderModel { Contractor = "K1", Name = $"ZS-{i + 1}/24" });
            //});

            //return models;
        }

        public async Task<HttpResponseModel> TestAddHeader()
        {            
            return await ProcessRequest(null, Enums.RequestType.TestAddOrderHeader);
        }

        public async Task<HttpResponseModel> TestAddItem(OrderDto dto)
        {
            return await ProcessRequest(dto, Enums.RequestType.TestAddOrderItem);
        }

        public async Task<HttpResponseModel> TestClose(OrderDto dto)
        {
            return await ProcessRequest(dto, Enums.RequestType.TestCloseOrder);
        }
    }

    #endif
}
