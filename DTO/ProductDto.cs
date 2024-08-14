using ProductInventoryApp.Models;

namespace ProductInventoryApp.DTO
{
    public class ProductRequestDto
    {
        // public string Id { get; set; }

       // public int InventoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

       

        public string Category { get; set; }
        public string? ProductUrl { get; set; }

        public string Availability { get; set; }
        public string Description { get; set; } = string.Empty;

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public decimal Total {  get; set; }

        public decimal ProductVat { get; set; }

        public decimal ProductProfit { get; set; }

        public decimal ProductUnitSellingPrice {  set; get; }




        // Computed property
        // Computed property: VAT is 2% of the price
        public decimal Vat => Price * 0.02m;

        // Computed property: Profit is 20% of the price
        public decimal Profit => Price * 0.20m;

        // Computed property: unit selling price is the sum of the price, VAT, and profit
        public decimal UnitSellingPrice => Price + Vat + Profit;

        public decimal TotalAmount => ProductUnitSellingPrice * Quantity;

    }
}
