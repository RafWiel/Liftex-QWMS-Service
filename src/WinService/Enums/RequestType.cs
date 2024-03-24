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
        FirstOrderRequest = 11, //order first
        TestAddOrderHeader = 11,
        TestAddOrderItem = 12,
        TestCloseOrder = 13,
        LastOrderRequest = 13, //order last
    }
}
