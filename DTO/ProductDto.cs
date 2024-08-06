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

        public string Description { get; set; } = string.Empty;

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }


        // Computed property
        public decimal TotalAmount
        {
            get { return Price * Quantity; }
        }
    }
}
