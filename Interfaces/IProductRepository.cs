using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.Common;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Interfaces
{
    public interface IProductRepository
    {
       public IQueryable<Product> GetProducts();
        public IQueryable<Product> GetPaginationProducts();

        public IQueryable<Product> GetPaginatedProducts(int pageSize, int page);

        public Task<PaginatedList<Product>> GetAllProductWithPagination(int page, int pageSize);
       public Task<Product> GetById(string Productid);

      public Product GetByName(string name);

      public  Product GetByDescription(string description);
       public Product GetByPrice(decimal price);
       public Product GetByQuantity(int quantity);
      public  bool ProductExists(string id);
        public bool CreateProduct(Product product);
        public  bool Save();

        public Task<int> TotalProductCount(); 
        public Task<bool> UpdateProduct(Product product);

        public Task<Product> DeleteProduct(Product product);


        public Task<Product> Add(Product product);

      public  Task<int> SaveChanges();

       Task<Product> Delete(string id);
        //Task<Product> Update(int id);
    }
}
