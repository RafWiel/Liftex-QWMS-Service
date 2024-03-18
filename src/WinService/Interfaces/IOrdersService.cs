using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMS.Models.Orders;
using WinService.DataTransferObjects;
using WinService.Models;

#nullable enable
namespace WinService.Interfaces
{
    public interface IOrdersService
    {
        Task<List<OrderListModel>?> Get(string? search, int? page);
        Task<HttpResponseModel> TestAddHeader();
    }
}
