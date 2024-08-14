using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;

namespace ProductInventoryApp
{
    public class SupplierSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {

            var supplierRepository = serviceProvider.GetRequiredService<ISuppliersRepository>();
            var retrievedProducts = supplierRepository.GetSuppliers();
            if (retrievedProducts.Any())
            {
                return;   // Data was already seeded
            }

            var suppliers = new List<Suppliers>
            {
                new Suppliers { Id = "Guid.NewGuid()" , Name = "Kofi", Price = 10.0m, Quantity = 100, ImageUrl="pgote", Category="book", Product="toffee"  ,Contact="2345678902", SupplierId="1234", SupplyType="delivery", },
               
            };

            foreach (var supplier in suppliers)
            {
                supplierRepository.Add(supplier);
            }
        }

    }
}
