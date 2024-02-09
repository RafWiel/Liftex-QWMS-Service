using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinService.ApiServices;
using WinService.CdnApi;
using WinService.Configuration;
using WinService.Models;

namespace WinService.Services
{
    public class CdnApiService : BaseService
    {
        #region Initialization        

        private CdnApiClient _api = new CdnApiClient();
        protected Thread _thread;
        protected ManualResetEvent _threadCancelEvent = new ManualResetEvent(false);

        public WinConfiguration.ApiConfiguration ApiConfiguration { get; set; }        
        public WinConfiguration.DatabaseConfiguration DatabaseConfiguration { get; set; }

        public Queue<Models.IpcRequestModel> Requests { get; private set; } = new Queue<Models.IpcRequestModel>();        
        
        private OrdersCdnApiService _ordersService;
        //private DocumentsApiService _documentsService;
        //private ContractorsApiService _contractorsService;

        public CdnApiService()
        {
            _api.LogErrorEvent += InvokeLogError;            
        }

        #endregion

        #region Methods
        
        public override void Start()
        {
            InitializeOrdersService();
            //InitializeDocumentsService();
            //InitializeContractorsService();           

            _thread = new Thread(new ThreadStart(Run));
            _thread.Start();
        }

        public override void Stop()
        {
            var isLoggedIn = _api.IsLoggedIn;

            _threadCancelEvent.Set();

            if (_thread == null)
                return;

            _thread.Join(5000);
            _thread = null;

            Thread t = new Thread(new ThreadStart(DetachApi));
            t.Start();
        }        

        private void Run()
        {            
            //w wersji produkcyjnej zaloguj sie raz
            #if !DEBUG
                _api.Login(ApiConfiguration.KeyServer, ApiConfiguration.DatabaseName, ApiConfiguration.User, ApiConfiguration.Password);
            #endif

            while (true)
            {
                try
                {
                    if (_threadCancelEvent.WaitOne(100, true) == true)
                        break;

                    if (Requests.Count == 0)
                        continue;

                    var request = Requests.Dequeue();

                    //if (_ordersService.ProcessRequest(request))
                    //    continue;

                    //if (_documentsService.ProcessRequest(request))
                    //    continue;

                    //if (_contractorsService.ProcessRequest(request))
                    //    continue;                    
                }
                catch (Exception ex)
                {
                    InvokeLogError(ex.Message);                    
                }
            }

            _api.Logout();
        }        

        private void DetachApi()
        {
            CdnApiClient.AttachThreadToClarion(0);
        }

        private void InitializeOrdersService()
        {
            _ordersService = new OrdersCdnApiService
            {
                Api = _api,
                ApiConfiguration = ApiConfiguration,
                DatabaseConfiguration = DatabaseConfiguration
            };

            _ordersService.LogError += InvokeLogError;
            _ordersService.LogEvent += InvokeLogEvent;
        }

        //private void InitializeDocumentsService()
        //{
        //    _documentsService = new DocumentsApiService
        //    {
        //        Api = _api,
        //        ApiConfiguration = ApiConfiguration,
        //        DatabaseConfiguration = DatabaseConfiguration
        //    };

        //    _documentsService.LogError += InvokeLogError;
        //    _documentsService.LogEvent += InvokeLogEvent;
        //}

        //private void InitializeContractorsService()
        //{
        //    _contractorsService = new ContractorsApiService
        //    {
        //        Api = _api,
        //        ApiConfiguration = ApiConfiguration,
        //        DatabaseConfiguration = DatabaseConfiguration
        //    };

        //    _contractorsService.LogError += InvokeLogError;
        //    _contractorsService.LogEvent += InvokeLogEvent;
        //}        

        #endregion
    }
}
