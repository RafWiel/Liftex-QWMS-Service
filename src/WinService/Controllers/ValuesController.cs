using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WinService.Controllers
{
    public class ValuesController : ApiController
    {
        //[HttpGet, Route("{id}/flows/config")]
        [HttpGet]
        [Route("api/v1/values/{value}")]
        public string Get(int value)
        {
            return $"Your value is {value}";
        }
    }
}
