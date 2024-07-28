using ProductInventoryApp.Models;

namespace ProductInventoryApp.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetById(int Productid);

        Product GetByName(string name);

        Product GetByDescription(string description);
        Product GetByPrice(decimal price);
        Product GetByQuantity(int quantity);
        bool ProductExists(int id);
        Product CreateProduct(Product product);
        bool Save();


        bool UpdateProduct(Product product);

        Task<Product> DeleteProduct(Product product);


        void Add(Product product);
        void Delete(int id);
    }
}
