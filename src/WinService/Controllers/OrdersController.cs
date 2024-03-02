﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WinService.Interfaces;
using WinService.Models;

namespace WinService.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }        

        [HttpGet]
        [Route("api/v1/orders")]
        public async Task<HttpResponseMessage> Get([FromUri] string? search = null, int? page = null)
        {
            var models = await _service.Get(search, page);
            if (models == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not found");

            return Request.CreateResponse(HttpStatusCode.OK, models);
        }
    }
}
