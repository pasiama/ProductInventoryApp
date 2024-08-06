using System.ComponentModel.DataAnnotations;

namespace ProductInventoryApp.Models
{

    //Models are normally reffered to as the POCO. This is where the getters and setters are. they are also classes with properties.
    // they reference our database as well. contents here are also seen in the database
    public class Product
    {

        // public int Id {  get; set; }

        //public Guid Uid { get; set; } = Guid.NewGuid();

        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        //public int InventoryId { get; set; } 
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price {  get; set; }
       
        public int Quantity {  get; set; }
        public decimal Total { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        // Computed property
        public decimal TotalAmount
        {
            get { return Price * Quantity; }
        }
    }
}
