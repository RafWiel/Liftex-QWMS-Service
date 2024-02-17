using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinService.Models.Products
{
    public class ProductDetailsCountModel
    {
        public string WarehouseCode { get; set; } = string.Empty;
        public decimal SaleCount { get; set; }
        public decimal WarehouseCount { get; set; }
        public decimal ReservationCount { get; set; }
    }
}
