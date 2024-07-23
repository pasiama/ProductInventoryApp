using ProductInventoryApp.Models;

namespace ProductInventoryApp.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
