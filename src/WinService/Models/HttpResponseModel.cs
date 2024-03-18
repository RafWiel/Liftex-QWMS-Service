using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinService.Models
{
    public class HttpResponseModel
    {        
        public HttpStatusCode Status { get; set; }        
        public object Content { get; set; }
        public string ErrorMessage { get; set; }

        public HttpResponseModel(HttpStatusCode status)
        {
            Status = status;
        }

        public HttpResponseModel(HttpStatusCode status, string errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public HttpResponseModel(object content)
        {
            Status = HttpStatusCode.OK;
            Content = content;
        }
    }
}
