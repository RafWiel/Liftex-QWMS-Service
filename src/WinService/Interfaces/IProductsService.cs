using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.Models.Products;

namespace WinService.Interfaces
{
    #nullable enable

    public interface IProductsService
    {
        Task<List<ProductListModel>?> GetProducts();
        Task<ProductDetailsModel?> GetProduct(string ean);        
    }
}
