using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinService.Models
{
    public class CdnApiResponseModel
    {
        public int? Id { get; set; }
        public int ErrorCode { get; set; }        
        public string ErrorMessage { get; set; }
    }
}
