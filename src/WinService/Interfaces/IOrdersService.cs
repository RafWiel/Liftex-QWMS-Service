using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.Models.Orders;

#nullable enable
namespace WinService.Interfaces
{
    public interface IOrdersService
    {
        Task<List<OrderModel>?> Get(string? search);
    }
}
