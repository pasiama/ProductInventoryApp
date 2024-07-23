using System.ComponentModel.DataAnnotations;

namespace ProductInventoryApp.Models
{

    //Models are normally reffered to as the POCO. This is where the getters and setters are. they are also classes with properties.
    // they reference our database as well. contents here are also seen in the database
    public class Product
    {
        [Key]
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description {  get; set; } 
        public decimal Price {  get; set; }
        public int Quantity {  get; set; }
    }
}
