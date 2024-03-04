using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMS.Models.Barcodes;

namespace WinService.Interfaces
{
    #nullable enable

    public interface IBaseService
    {        
        void Start();
        void Stop();
    }
}
