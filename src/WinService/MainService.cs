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

namespace WinService
{
    public class MainService
    {
        #region Initialization

        private WinConfiguration _config;                
        private MonitorWcfClient _monitorWcfClient = new MonitorWcfClient();
        
        private OrdersService _ordersService;
        private CdnApiService _cdnApiService;
        private WebApiService _webApiService;

        #endregion

        #region Methods

        public void Start()
        {
            try
            {
                var cfgFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DataSoft", WinConfiguration.FileName);
                var logFilePath = cfgFilePath.Replace(".cfg", ".log");

                gLog.FilePath = logFilePath;

                LogEvent($"Uruchamianie usługi - ver {Assembly.GetExecutingAssembly().GetName().Version}");

                _config = WinConfiguration.Load(cfgFilePath);
                
                InitializeCdnApiService();
                InitializeOrdersService();

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
            _ordersService.Stop();
            _cdnApiService.Stop();            
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

            using (var db = new DatabaseClient(_config.Database))
            {
                db.ErrorLog_Insert(message);
            }

            _monitorWcfClient.LogError(message);
            gLog.Write(message);
        }

        private void InitializeWebApiService()
        {
            _webApiService = new WebApiService
            {
                OrdersService = _ordersService
            };

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

        private void InitializeOrdersService()
        {
            _ordersService = new OrdersService();
            //{
            //    DatabaseConfiguration = _config.Database,
            //    Requests = _cdnApiService.Requests,
            //};

            _ordersService.Start();

            _ordersService.LogEvent += LogEvent;
            _ordersService.LogError += LogError;
        }        

        #endregion
    }
}
