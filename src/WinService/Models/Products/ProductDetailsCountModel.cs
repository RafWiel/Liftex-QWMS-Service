namespace QWMS.Models.Products
{
    public class ProductDetailsCountModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = string.Empty;
        public decimal SaleCount { get; set; }
        public decimal WarehouseCount { get; set; }
        public decimal ReservationCount { get; set; }
        public int MeasureUnitDecimalPlaces { get; set; }        
    }
}
