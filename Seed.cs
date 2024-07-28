using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;
using Microsoft.Extensions.DependencyInjection;

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
                new Product { Id = 1, Name = "Product1", Price = 10.0m, Quantity = 100 },
                new Product { Id = 2, Name = "Product2", Price = 20.0m, Quantity = 200 },
                new Product { Id = 3, Name = "Product3", Price = 30.0m, Quantity = 300 }
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
