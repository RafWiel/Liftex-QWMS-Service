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
using WinService.DataTransferObjects;
using WinService.Models;

namespace WinService.Services
{
    public class BaseRequestsService : BaseService
    {
        #region Initialization

        public WinService.Configuration.DatabaseConfiguration DatabaseConfiguration { get; set; }
        public Queue<Models.CdnApiRequestModel> Requests { get; set; }

        #endregion

        #region Methods

        protected async Task<HttpResponseModel> ProcessRequest(object webRequest, Enums.RequestType requestType)
        {
            var result = await Task.Run(() =>
            {
                try
                {

                    var request = new Models.CdnApiRequestModel
                    {
                        Id = Guid.NewGuid(),
                        ProcessedEvent = new ManualResetEvent(false),
                        WebRequest = webRequest,
                        Type = requestType
                    };

                    Requests.Enqueue(request);
                    request.ProcessedEvent.WaitOne(60000, true);

                    if (request.Response == null)
                        return new HttpResponseModel(HttpStatusCode.RequestTimeout, "Request timeout");

                    if (request.Response.ErrorCode != 0)
                        return new HttpResponseModel(HttpStatusCode.InternalServerError, $"{request.Response.ErrorCode}: {request.Response.ErrorMessage}");

                    var dto = new IdResponseDto
                    {
                        Id = request.Response.Id.Value
                    };

                    return new HttpResponseModel(dto);
                }
                catch (Exception ex)
                {
                    InvokeLogError(ex.Message);
                    return new HttpResponseModel(HttpStatusCode.InternalServerError, "Internal server error occured");
                }
            });

            return result;
        }

        #endregion
    }
}
