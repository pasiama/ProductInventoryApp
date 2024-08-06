using Microsoft.AspNetCore.Mvc;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Services.Interfaces
{
    public interface IProductServices 
    {
       public  Task<List<Product>> GetProducts();



       public Task<Product> GetProduct(string id);

       public Task<Product> DeleteProduct(string id);

       public bool ProductExists(string id);
       public Task<bool> UpdateProduct(string id, ProductRequestDto productDto);

      public  Task<Product> Add(ProductRequestDto productDto);

       public Task<Product>CreateProduct(ProductRequestDto productDto);

        //public  Task<Product> Delete(string id);
    }
}
