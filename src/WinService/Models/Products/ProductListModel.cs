using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinService.Models.Products
{
    public class ProductListModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;        
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public int MeasureUnitDecimalPlaces { get; set; }
    }
}
