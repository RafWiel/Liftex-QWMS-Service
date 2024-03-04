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
using QWMS.Models.Reservations;

namespace WinService.Services
{
    #if !MOCKUP

    public class ReservationsService : BaseService, IReservationsService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
        public PropertiesConfiguration PropertiesConfiguration { get; set; } = new PropertiesConfiguration();

        public async Task<List<ReservationListModel>?> Get(int productId, int? page)
        {
            try
            {
                using (var db = new CdnDatabaseClient(DatabaseConfiguration))
                {
                    db.LogError += InvokeLogError;

                    return await db.GetReservations(productId, PropertiesConfiguration.WarehouseId, page);
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
