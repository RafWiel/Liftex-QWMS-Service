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
    #if !MOCKUP

    public class BarcodesService : BaseService, IBarcodesService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
        
        public async Task<List<BarcodeListModel>?> Get(int productId, int? page)
        {
            try
            {
                using (var db = new CdnDatabaseClient(DatabaseConfiguration))
                {
                    db.LogError += InvokeLogError;

                    return await db.GetBarcodes(productId, page);
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
