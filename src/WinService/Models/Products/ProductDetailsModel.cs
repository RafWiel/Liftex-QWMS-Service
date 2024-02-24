using System.Collections.Generic;

namespace QWMS.Models.Products
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Ean { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public int MeasureUnitDecimalPlaces { get; set; }

        public List<ProductDetailsCountModel> Items { get; set; } = new List<ProductDetailsCountModel>();
    }
}
