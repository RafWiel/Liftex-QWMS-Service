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
using WinService.Models;

namespace WinService.Services
{
    public class OrdersService : BaseService, IOrdersService
    {
        public WinConfiguration.DatabaseConfiguration? DatabaseConfiguration { get; set; }

        public async Task<List<OrderModel>?> Get(string? search)
        {
            try
            {
                using (var db = new DatabaseClient(DatabaseConfiguration))
                {
                    db.LogError += InvokeLogError;

                    return await db.GetOrders();                    
                }
            }
            catch (Exception ex)
            {
                InvokeLogError(ex.ToString());                
            }

            return null;
        }

        public async Task<List<OrderModel>> Get1(string? search)
        {
            var models = new List<OrderModel>();            

            //na razie Task.Run, ale docelowo cmd.ExecuteReaderAsync
            await Task.Run(() => {                
                for (int i = 0; i < 1000; i++)
                    models.Add(new OrderModel { Contractor = "K1", Name = $"ZS-{i + 1}/24" });                
            });

            return models;
        }
    }
}
