﻿using System;
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
    public class ProductsController : ApiController
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }        

        [HttpGet]
        [Route("api/v1/products")]
        public async Task<HttpResponseMessage> Get([FromUri] string? search = null, int? page = null)
        {
            var model = await _service.Get(search, page);
            if (model == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not found");

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/v1/product")]
        public async Task<HttpResponseMessage> GetSingle([FromUri] string ean)
        {
            var model = await _service.GetSingle(ean);
            if (model == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not found");

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
