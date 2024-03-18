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
        TestAddOrderHeader = 11,
        TestAddOrderItems = 12,
        TestCloseOrder = 13,
    }
}
