using System.ComponentModel.DataAnnotations;
using System.Numerics;

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

        //selling price
        public decimal Price {  get; set; }
        
      

        public int Quantity {  get; set; }
       

        public string Category { get; set; }
        public string? ProductUrl { get; set; }

        public string Availability { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; } 
        public DateTime? CreatedAt { get; } = DateTime.Now;
        public decimal Total { get; set; }

        public decimal ProductVat { get; set; }

        public decimal ProductProfit { get; set; }

        public decimal ProductUnitSellingPrice { set; get; }


        // Computed property: VAT is 2% of the price
        public decimal Vat => Price * 0.02m;

        // Computed property: Profit is 20% of the price
        public decimal Profit => Price * 0.20m;

        // Computed property: unit selling price is the sum of the price, VAT, and profit
        public decimal UnitSellingPrice => Price + Vat + Profit;

        public decimal TotalAmount 
        {
            get { return UnitSellingPrice * Quantity; } }
    }
}
