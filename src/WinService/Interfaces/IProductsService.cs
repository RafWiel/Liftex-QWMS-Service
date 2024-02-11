using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.Models;

namespace WinService.Interfaces
{
    #nullable enable
    public interface IProductsService
    {
        Task<ProductDetailsModel?> GetProductDetails(string ean);        
    }
}
