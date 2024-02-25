using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMS.Models.Products;

namespace WinService.Interfaces
{
    #nullable enable

    public interface IProductsService
    {
        Task<List<ProductListModel>?> Get(string? search, int? page);
        Task<ProductDetailsModel?> GetSingle(string ean);                       
    }
}
