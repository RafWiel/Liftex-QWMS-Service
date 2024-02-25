﻿#nullable enable
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
using QWMS.Models.Products;

namespace WinService.Services
{
    #if !LOCAL

    public class ProductsService : BaseService, IProductsService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
        public PropertiesConfiguration PropertiesConfiguration { get; set; } = new PropertiesConfiguration();

        public async Task<List<ProductListModel>?> Get(string? search, int? page)
        {
            try
            {
                using (var db = new CdnDatabaseClient(DatabaseConfiguration))
                {
                    db.LogError += InvokeLogError;

                    return await db.GetProducts(PropertiesConfiguration.WarehouseId, search, page);
                }
            }
            catch (Exception ex)
            {
                InvokeLogError(ex.ToString());
            }

            return null;
        }

        public async Task<ProductDetailsModel?> GetSingle(string ean)
        {
            try
            {
                using (var db = new CdnDatabaseClient(DatabaseConfiguration))
                {
                    db.LogError += InvokeLogError;

                    var id = await db.GetProductId(ean);
                    if (id == null)
                        return null;

                    return await db.GetProduct(id.Value, PropertiesConfiguration.WarehouseId);
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
