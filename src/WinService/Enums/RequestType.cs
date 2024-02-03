using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinService.Enums
{
    public enum RequestType
    {
        AddOrder = 1,        
        AddContractor = 2,
        AddContractorAddress = 3,
        DeleteContractorAddress = 4,
        AddDocument = 5,
        AddInvoiceToReceiptDocument = 6,
        AddCorrectionDocument = 7,
        AddComplaint = 8,
    }
}
