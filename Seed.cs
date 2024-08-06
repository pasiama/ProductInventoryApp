using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProductInventoryApp
{
    public class Seed
    {

        public static async void Initialize(IServiceProvider serviceProvider)
        {

            var productRepository = serviceProvider.GetRequiredService<IProductRepository>();
            var retrievedProducts = await productRepository.GetProducts();
            if (retrievedProducts.Any())
            {
                return;   // Data was already seeded
            }

            var products = new List<Product>
            {
                new Product { Id = "Guid.NewGuid()" , Name = "Product1", Price = 10.0m, Quantity = 100, CreatedBy= "Phoebe", UpdatedBy="Dela" , UpdatedAt=DateTime.Now, Total=10.0m},
                new Product { Id = "Guid.NewGuid()"  , Name = "Product2", Price = 20.0m, Quantity = 200, CreatedBy= "Phoebe", UpdatedBy="Dela", UpdatedAt=DateTime.Now, Total=10.0m },
                new Product {Id = "Guid.NewGuid()", Name = "Product3", Price = 30.0m, Quantity = 300, CreatedBy= "Phoebe", UpdatedBy="Dela", UpdatedAt=DateTime.Now, Total=10.0m }
            };

            foreach (var product in products)
            {
                productRepository.Add(product);
            }
        }

        internal void SeedApplicationContext(ServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
