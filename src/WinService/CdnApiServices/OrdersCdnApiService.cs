using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.CdnApi;
using WinService.Configuration;
using WinService.Database;
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
            if (request.Type != Enums.RequestType.AddTestOrder)
                return false;

            try
            {
                if (Api.IsLoggedIn == false)
                    Api.Login(ApiConfiguration.KeyServer, ApiConfiguration.DatabaseName, ApiConfiguration.User, ApiConfiguration.Password);

                if (request.Type == Enums.RequestType.AddTestOrder)
                    AddTestOrder(request);                
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

        private void AddTestOrder(Models.CdnApiRequestModel request)
        {
            int documentId = 0;

            using (var db = new CdnDatabaseClient(DatabaseConfiguration))
            {
                db.LogError += InvokeLogError;

                bool result = AddTestHeader(request, db, ref documentId);
                if (!result)
                    return;

                result = AddTestItems(request, db, documentId);
                if (!result)
                    return;
            }

            CloseTestOrder(request, documentId);
        }

        private bool AddTestHeader(Models.CdnApiRequestModel request, CdnDatabaseClient database, ref int documentId)
        {            
            var errorMessage = string.Empty;

            int result = Api.AddTestOrder(ref documentId, ref errorMessage);
            if (result != 0)
            {
                SetErrorResponse(request, result, errorMessage);
                return false;
            }

            return true;
        }

        private bool AddTestItems(Models.CdnApiRequestModel request, CdnDatabaseClient database, int documentId)
        {
            var errorMessage = string.Empty;
                
            var result = Api.AddTestOrderItem(documentId, ref errorMessage);
            if (result != 0)
            {
                //anuluj dokument
                Api.CloseTestOrder(true, ref documentId, ref errorMessage);

                SetErrorResponse(request, result, errorMessage);             
            }
            
            //wystapil blad pozycji towaru
            if (request.Response != null)
                return false;

            return true;
        }

        private bool CloseTestOrder(Models.CdnApiRequestModel request, int documentId)
        {
            var errorMessage = string.Empty;

            var result = Api.CloseTestOrder(false, ref documentId, ref errorMessage);
            if (result != 0)
            {
                SetErrorResponse(request, result, errorMessage);
                return false;
            }

            request.Response = new CdnApiResponseModel
            {
                Id = documentId
            };

            return true;
        }            
    }
}
