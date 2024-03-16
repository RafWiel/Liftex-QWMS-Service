using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinService.Enums;

namespace WinService.Models
{
    public class CdnApiRequestModel
    {
        public Guid Id { get; set; }
        public ManualResetEvent ProcessedEvent { get; set; }
        public object WebRequest { get; set; }
        public RequestType Type { get; set; }
        public IpcResponseModel Response { get; set; }
    }    
}
