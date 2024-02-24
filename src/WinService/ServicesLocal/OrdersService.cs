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
using QWMS.Models.Orders;

namespace WinService.Services
{
    #if LOCAL

    public class OrdersService : BaseService, IOrdersService
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();

        public async Task<List<OrderListModel>?> Get(string? search)
        {
            var models = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                
                var models = new List<OrderListModel>();

                for (int i = 1; i <= 100; i++)
                {
                    models.Add(new OrderListModel()
                    {
                        Id = i,
                        Contractor = $"K1",
                        Name = $"ZS-{i}/24"                        
                    });
                }

                return models;
            });

            return models;
        }
    }

    #endif
}