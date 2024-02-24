using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMS.Models.Orders;

#nullable enable
namespace WinService.Interfaces
{
    public interface IOrdersService
    {
        Task<List<OrderListModel>?> Get(string? search);
    }
}
