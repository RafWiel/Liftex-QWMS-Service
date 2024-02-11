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
    public class ProductsService : BaseService, IProductsService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
        public PropertiesConfiguration PropertiesConfiguration { get; set; } = new PropertiesConfiguration();

        public async Task<ProductDetailsModel?> GetProductDetails(string ean)
        {
            try
            {
                using (var db = new CdnDatabaseClient(DatabaseConfiguration))
                {
                    db.LogError += InvokeLogError;

                    var id = await db.GetProductId(ean);
                    if (id == null)
                        return null;

                    return await db.GetProductDetails(id.Value, PropertiesConfiguration.WarehouseId);
                }
            }
            catch (Exception ex)
            {
                InvokeLogError(ex.ToString());
            }

            return null;

            //return Task.Run(() =>
            //{                
            //    ProductModel? model = null;                
            //    Thread.Sleep(1000);

            //    var xmodel = new ProductModel()
            //    {
            //        Code = $"TX{index}",
            //        Name = $"TowarX {index}",
            //        Ean = $"12345{index}",
            //        Price = index * 0.1M,
            //        Count = index
            //    };

            //    index++;

            //    return xmodel ?? model;
            //});            
        }
    }
}
