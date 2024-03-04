using InterProcessCommunication.Clients;
using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinService.Models;
using WinService.Configuration;
using WinService.Services;
using WinService.Database;
using System.Web.Http.SelfHost;
using System.Web.Http.Routing;
using WinService.Controllers;
using WinService.Helpers;
using WinService.Interfaces;

namespace WinService
{
    public class MainService
    {
        #region Initialization

        private Configuration.Configuration _config;                
        private MonitorWcfClient _monitorWcfClient = new MonitorWcfClient();
                
        private CdnApiService _cdnApiService;
        private WebApiService _webApiService;

        private BarcodesService _barcodesService;
        private OrdersService _ordersService;
        private ProductsService _productsService;
        private ReservationsService _reservationsService;

        #endregion

        #region Methods

        public void Start()
        {
            try
            {
                var cfgFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DataSoft", Configuration.Configuration.FileName);
                var logFilePath = cfgFilePath.Replace(".cfg", ".log");

                gLog.FilePath = logFilePath;

                LogEvent($"Uruchamianie usługi - ver {Assembly.GetExecutingAssembly().GetName().Version}");

                _config = Configuration.Configuration.Load(cfgFilePath);
                
                InitializeCdnApiService();

                InitializeBarcodesService();
                InitializeOrdersService();
                InitializeProductsService();
                InitializeReservationsService();

                InitializeWebApiService();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());
            }
        }

        public void Stop()
        {
            LogEvent("Zatrzymywanie usługi");

            _webApiService.Stop();

            _barcodesService.Stop();
            _ordersService.Stop();
            _productsService.Stop();
            _reservationsService.Stop();
            
            _cdnApiService.Stop();            
        }

        private void InitializeWebApiService()
        {
            _webApiService = new WebApiService
            (
                _barcodesService,
                _ordersService,
                _productsService,
                _reservationsService
            );

            _webApiService.Start();
        }

        private void InitializeCdnApiService()
        {
            _cdnApiService = new CdnApiService
            {
                DatabaseConfiguration = _config.Database,
                ApiConfiguration = _config.Api,
            };

            _cdnApiService.Start();

            _cdnApiService.LogEvent += LogEvent;
            _cdnApiService.LogError += LogError;
        }

        private void InitializeBarcodesService()
        {
            _barcodesService = new BarcodesService
            {
                DatabaseConfiguration = _config.Database,
            };

            _barcodesService.Start();

            _barcodesService.LogEvent += LogEvent;
            _barcodesService.LogError += LogError;
        }

        private void InitializeOrdersService()
        {
            _ordersService = new OrdersService
            {
                DatabaseConfiguration = _config.Database,                
            };

            _ordersService.Start();

            _ordersService.LogEvent += LogEvent;
            _ordersService.LogError += LogError;
        }

        private void InitializeProductsService()
        {
            _productsService = new ProductsService
            {
                DatabaseConfiguration = _config.Database,
                PropertiesConfiguration = _config.Properties
            };

            _productsService.Start();

            _productsService.LogEvent += LogEvent;
            _productsService.LogError += LogError;
        }

        private void InitializeReservationsService()
        {
            _reservationsService = new ReservationsService
            {
                DatabaseConfiguration = _config.Database,
                PropertiesConfiguration = _config.Properties
            };

            _reservationsService.Start();

            _reservationsService.LogEvent += LogEvent;
            _reservationsService.LogError += LogError;
        }

        private void LogEvent(string message)
        {
            Console.WriteLine(message);
            _monitorWcfClient.LogEvent(message);
            gLog.Write(message);
        }

        private void LogError(string message)
        {
            Console.WriteLine(message);

            using (var db = new CdnDatabaseClient(_config.Database))
            {
                db.ErrorLog_Insert(message);
            }

            _monitorWcfClient.LogError(message);
            gLog.Write(message);
        }

        #endregion
    }
}
