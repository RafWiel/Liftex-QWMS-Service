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
        public WinConfiguration.ApiConfiguration ApiConfiguration { get; set; }
        public WinConfiguration.DatabaseConfiguration DatabaseConfiguration { get; set; }

        //public bool ProcessRequest(Models.IpcRequestModel request)
        //{
        //    if (request.Type != Enums.RequestType.AddOrder)
        //        return false;

        //    int documentId = 0;

        //    try
        //    {
        //        if (Api.IsLoggedIn == false)
        //            Api.Login(ApiConfiguration.KeyServer, ApiConfiguration.DatabaseName, ApiConfiguration.User, ApiConfiguration.Password);                

        //        using (var db = new DatabaseClient(DatabaseConfiguration))
        //        {
        //            db.LogError += InvokeLogError;

        //            bool result = AddHeader(request, db, ref documentId);
        //            if (!result)
        //                return true;

        //            result = AddItems(request, db, documentId);
        //            if (!result)
        //                return true;
        //        }

        //        CloseDocument(request, documentId);
        //    }
        //    catch (Exception ex)
        //    {
        //        InvokeLogError(ex.Message);
        //    }
        //    finally
        //    {
        //        request.ProcessedEvent.Set();
        //    }

        //    return true;
        //}

        //private bool AddHeader(Models.IpcRequestModel request, DatabaseClient database, ref int documentId)
        //{
        //    if (!ValidateContractors(request, database))
        //        return false;

        //    var errorMessage = string.Empty;

        //    int result = Api.AddOrder((InterProcessCommunication.Models.OrderRequestModel)request.WebRequest, ref documentId, ref errorMessage);
        //    if (result != 0)
        //    {
        //        SetErrorResponse(request, result, errorMessage);
        //        return false;
        //    }

        //    return true;
        //}

        //private bool AddItems(Models.IpcRequestModel request, DatabaseClient database, int documentId)
        //{
        //    var errorMessage = string.Empty;

        //    foreach (var item in ((InterProcessCommunication.Models.OrderRequestModel)request.WebRequest).Items)
        //    {
        //        //var unitParameters = db.GetUnitParameters(item);

        //        var result = Api.AddOrderItem(item, documentId, ref errorMessage);
        //        if (result != 0)
        //        {
        //            //anuluj dokument
        //            Api.CloseOrder(true, ref documentId, ref errorMessage);

        //            SetErrorResponse(request, result, errorMessage);
        //            break;
        //        }
        //    }

        //    //wystapil blad pozycji towaru
        //    if (request.Response != null)
        //        return false;

        //    return true;
        //}

        //private bool CloseDocument(Models.IpcRequestModel request, int documentId)
        //{
        //    var errorMessage = string.Empty;

        //    var result = Api.CloseOrder(false, ref documentId, ref errorMessage);
        //    if (result != 0)
        //    {
        //        SetErrorResponse(request, result, errorMessage);
        //        return false;
        //    }

        //    request.Response = new IpcResponseModel
        //    {
        //        Id = documentId
        //    };

        //    return true;
        //}

        //private bool ValidateContractors(Models.IpcRequestModel request, DatabaseClient database)
        //{
        //    var webRequest = (InterProcessCommunication.Models.OrderRequestModel)request.WebRequest;

        //    if (!ValidateContractor(request, database, webRequest.Contractor.Main))
        //        return false;

        //    if (!ValidateContractor(request, database, webRequest.Contractor.Target))
        //        return false;

        //    if (!ValidateContractor(request, database, webRequest.Contractor.Payer, true))
        //        return false;

        //    GetContractorExpoNorm(request, database);

        //    return true;
        //}

        //private bool ValidateContractor(Models.IpcRequestModel request, DatabaseClient database, string code, bool isPayer = false)
        //{
        //    if (string.IsNullOrEmpty(code))
        //        return true;

        //    var id = database.GetContractorId(code);
        //    if (id == 0)
        //    {
        //        SetErrorResponse(request, 285, $"Nie znaleziono kontrahenta {code}");
        //        return false;
        //    }

        //    if (isPayer)
        //        ((InterProcessCommunication.Models.OrderRequestModel)request.WebRequest).Contractor.PayerId = id;

        //    return true;
        //}

        //private void GetContractorExpoNorm(Models.IpcRequestModel request, DatabaseClient database)
        //{
        //    var webRequest = (InterProcessCommunication.Models.OrderRequestModel)request.WebRequest;
        //    var expoNorm = database.GetContractorExpoNorm(webRequest.Contractor.Main);

        //    switch (expoNorm)
        //    {
        //        case 2: //unijny
        //            expoNorm = 6; //transakcja wewnatrzwspolnotowa
        //            break;
        //        case 3: //pozaunijny
        //            expoNorm = 1; //transakcja inna zagraniczna
        //            break;
        //        default:
        //            expoNorm = 0; //transakcja krajowa
        //            break;
        //    }

        //    ((InterProcessCommunication.Models.OrderRequestModel)request.WebRequest).Contractor.ExpoNorm = expoNorm;
        //}        
    }
}
