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
        public WinConfiguration.DatabaseConfiguration? DatabaseConfiguration { get; set; }

        int index = 1;
        public Task<ProductModel?> GetOne(string ean)
        {
            return Task.Run(() =>
            {                
                ProductModel? model = null;                
                Thread.Sleep(1000);

                var xmodel = new ProductModel()
                {
                    Code = $"TX{index}",
                    Name = $"TowarX {index}",
                    Ean = $"12345{index}",
                    Price = index * 0.1M,
                    Count = index
                };

                index++;

                return xmodel ?? model;
            });            
        }
    }
}
