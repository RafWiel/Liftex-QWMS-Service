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
using QWMS.Models.Products;

namespace WinService.Services
{
#if MOCKUP

    public class BarcodesService : BaseService, IBarcodesService
    {        
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
        
        public async Task<List<BarcodeListModel>?> Get(int productId, int? page)
        {            
            var models = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                var models = new List<BarcodeListModel>();

                if (productId <= 0)
                    return models.ToList();

                int index = ((page ?? 1) - 1) * 10 + 1;

                for (int i = index; i <= index + 10; i++)
                {
                    models.Add(new BarcodeListModel()
                    {
                        Id = i,
                        Code = $"20101111{i:0000}",                        
                        MeasureUnit = "szt"
                    });
                }

                return models.ToList();
            });

            return models;
        }      
    }

#endif
}
