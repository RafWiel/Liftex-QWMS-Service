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
    #if LOCAL

    public class ProductsService : BaseService, IProductsService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
        public PropertiesConfiguration PropertiesConfiguration { get; set; } = new PropertiesConfiguration();

        public async Task<List<ProductListModel>?> Get()
        {
            var models = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                var models = new List<ProductListModel>();

                for (int i = 1; i <= 100; i++)
                {
                    models.Add(new ProductListModel()
                    {
                        Id = i,
                        Code = $"T{i}",
                        Name = $"Towar {i}",
                        Price = i * 10.1M,
                        Count = 1 * 2,
                        MeasureUnitDecimalPlaces = 3
                    });
                }

                return models;
            });

            return models;
        }

        public async Task<ProductDetailsModel?> GetSingle(string ean)
        {                        
            var model = await Task.Run(() =>
            {
                Thread.Sleep(2000);

                return new ProductDetailsModel()
                {
                    Id = 1,
                    Code = $"T1",
                    Name = $"Towar 1 dłuższa nazwa",
                    Ean = "2010000000014",
                    Price = 9999.99M,
                    Count = 9999,
                    MeasureUnitDecimalPlaces = 3,
                    Items = new List<ProductDetailsCountModel>
                    {
                        new ProductDetailsCountModel
                        {
                            WarehouseId = 1,
                            WarehouseCode = "Mag 1",
                            SaleCount = 10.5M,
                            WarehouseCount = 20,
                            ReservationCount = 33.5M,
                            MeasureUnitDecimalPlaces = 2
                        },
                        new ProductDetailsCountModel
                        {
                            WarehouseId = 2,
                            WarehouseCode = "Mag 2",
                            SaleCount = 101.5M,
                            WarehouseCount = 201,
                            ReservationCount = 333.5M,
                            MeasureUnitDecimalPlaces = 0
                        },
                        new ProductDetailsCountModel
                        {
                            WarehouseId = 3,
                            WarehouseCode = "Mag 3",
                            SaleCount = 0,
                            WarehouseCount = 0,
                            ReservationCount = 0,
                            MeasureUnitDecimalPlaces = 0
                        },
                        new ProductDetailsCountModel
                        {
                            WarehouseId = 4,
                            WarehouseCode = "Mag 4",
                            SaleCount = 0,
                            WarehouseCount = 0,
                            ReservationCount = 0,
                            MeasureUnitDecimalPlaces = 0
                        }                        
                    }
                };                
            });

            return model;
        }        
    }

    #endif
}
