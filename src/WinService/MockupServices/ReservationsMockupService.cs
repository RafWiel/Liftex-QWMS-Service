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

#nullable enable

namespace WinService.Services
{
    #if MOCKUP

    public class ReservationsService : BaseService, IReservationsService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();
        public PropertiesConfiguration PropertiesConfiguration { get; set; } = new PropertiesConfiguration();

        public async Task<List<ReservationListModel>?> Get(int productId, int? page)
        {
            var models = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                var models = new List<ReservationListModel>();

                if (productId <= 0)
                    return models.ToList();

                int index = ((page ?? 1) - 1) * 10 + 1;

                for (int i = index; i <= index + 10; i++)
                {
                    models.Add(new ReservationListModel()
                    {
                        Id = i,
                        Contractor = "K1",
                        OrderName = $"ZS-{i}/01/24",
                        Count = i * 10,
                        MeasureUnitDecimalPlaces = 1
                    });
                }

                return models.ToList();
            });

            return models;
        }
    }

    #endif
}
