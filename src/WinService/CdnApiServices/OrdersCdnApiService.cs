using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.CdnApi;
using WinService.Configuration;
using WinService.Database;
using WinService.DataTransferObjects;
using WinService.Models;
using WinService.Services;

namespace WinService.ApiServices
{
    public class OrdersCdnApiService : BaseService
    {
        public CdnApiClient Api { get; set; }
        public ApiConfiguration ApiConfiguration { get; set; }
        public DatabaseConfiguration DatabaseConfiguration { get; set; }

        public bool ProcessRequest(Models.CdnApiRequestModel request)
        {
            if (request.Type < Enums.RequestType.FirstOrderRequest ||
                request.Type > Enums.RequestType.LastOrderRequest)
                return false;

            try
            {
                if (Api.IsLoggedIn == false)
                {
                    InvokeLogEvent($"Logowanie API");
                    Api.Login(ApiConfiguration.KeyServer, ApiConfiguration.DatabaseName, ApiConfiguration.User, ApiConfiguration.Password);
                }

                if (request.Type == Enums.RequestType.TestAddOrderHeader)
                    TestAddOrderHeader(request);

                if (request.Type == Enums.RequestType.TestAddOrderItem)
                    TestAddOrderItem(request);

                if (request.Type == Enums.RequestType.TestCloseOrder)
                    TestCloseOrder(request);
            }
            catch (Exception ex)
            {
                InvokeLogError(ex.Message);
            }
            finally
            {
                request.ProcessedEvent.Set();
            }

            return true;
        }               

        private void TestAddOrderHeader(Models.CdnApiRequestModel request)
        {            
            var errorMessage = string.Empty;
            int documentId = 0;

            InvokeLogEvent($"Tworzenie nagłówka zamówienia. Kontrahent K1");

            var result = Api.AddTestOrder(ref documentId, ref errorMessage);
            if (result != 0)
            {
                SetErrorResponse(request, result, errorMessage);
                return;
            }

            request.Response = new CdnApiResponseModel
            {
                Id = documentId
            };
        }

        private void TestAddOrderItem(Models.CdnApiRequestModel request)
        {
            var errorMessage = string.Empty;

            var dto = (OrderDto)request.WebRequest;
            var id = dto.Id;

            InvokeLogEvent($"Dodawanie towaru do zamówienia. Kod T1");

            var result = Api.AddTestOrderItem(id, ref errorMessage);
            if (result != 0)
            {
                InvokeLogEvent($"Anulowanie zamówienia");

                //anuluj dokument
                Api.CloseTestOrder(true, ref id, ref errorMessage);

                SetErrorResponse(request, result, errorMessage);
            }

            //wystapil blad pozycji towaru
            if (request.Response != null)
                return;

            request.Response = new CdnApiResponseModel
            {
                Id = null
            };
        }

        private void TestCloseOrder(Models.CdnApiRequestModel request)
        {
            var errorMessage = string.Empty;

            var dto = (OrderDto)request.WebRequest;
            var id = dto.Id;

            InvokeLogEvent($"Zamykanie zamówienia");

            var result = Api.CloseTestOrder(false, ref id, ref errorMessage);
            if (result != 0)
            {
                SetErrorResponse(request, result, errorMessage);
                return;
            }

            request.Response = new CdnApiResponseModel
            {
                Id = id
            };
        }
    }
}
