using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<OrderModel>> Get() //string? search)
        {
            var items = await _service.Get(string.Empty);
            if (items == null)
                return null;
            //return NotFound();

            return items;
            //return Ok(models);

            zwroc jako HttpResponseMessage
        }
    }
}
