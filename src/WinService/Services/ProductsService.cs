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
using WinService.Models.Products;

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

            //Thread.Sleep(1000);
            
            //var model = new ProductDetailsModel()
            //{
            //    Id = 1,
            //    Code = $"T1",
            //    Name = $"Towar 1 dłuższa nazwa",
            //    Ean = "2010000000014",
            //    Price = 9999.99M,
            //    Count = 9999,
            //    Items = new List<ProductDetailsCountModel> 
            //    { 
            //        new ProductDetailsCountModel
            //        { 
            //            WarehouseCode = "Mag 1",
            //            SaleCount = 10.5M,
            //            WarehouseCount = 20,
            //            ReservationCount = 33.5M
            //        },
            //        new ProductDetailsCountModel
            //        {
            //            WarehouseCode = "Mag 2",
            //            SaleCount = 101.5M,
            //            WarehouseCount = 201,
            //            ReservationCount = 333.5M
            //        }
            //    }
            //};

            //return model;            
        }        
    }
}
