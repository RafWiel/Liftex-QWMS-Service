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
    #if !LOCAL

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
        }        
    }

    #endif
}
