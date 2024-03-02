namespace QWMS.Models.Products
{
    public class ProductListModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;        
        public decimal Count { get; set; }
        public decimal Price { get; set; }       
        public int MeasureUnitDecimalPlaces { get; set; }
    }
}
