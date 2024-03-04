using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WinService.Interfaces;
using WinService.Models;

#nullable enable

namespace WinService.Controllers
{
    public class ReservationsController : ApiController
    {
        private readonly IReservationsService _service;

        public ReservationsController(IReservationsService service)
        {
            _service = service;
        }        

        [HttpGet]
        [Route("api/v1/reservations")]
        public async Task<HttpResponseMessage> Get([FromUri(Name = "product-id")] int productId, int? page = null)
        {
            var models = await _service.Get(productId, page);
            if (models == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not found");

            return Request.CreateResponse(HttpStatusCode.OK, models);
        }
    }
}
