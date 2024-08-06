using ProductInventoryApp.DTO;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Interfaces
{
    public interface IProductRepository
    {
       public Task<List<Product>> GetProducts();
       public Task<Product> GetById(string Productid);

      public Product GetByName(string name);

      public  Product GetByDescription(string description);
       public Product GetByPrice(decimal price);
       public Product GetByQuantity(int quantity);
      public  bool ProductExists(string id);
        public bool CreateProduct(Product product);
      public  bool Save();


         public Task<bool> UpdateProduct(Product product);

        public Task<Product> DeleteProduct(Product product);


        public Task<Product> Add(Product product);

      public  Task<int> SaveChanges();

       Task<Product> Delete(string id);
        //Task<Product> Update(int id);
    }
}
