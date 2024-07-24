using ProductInventoryApp.Models;

namespace ProductInventoryApp.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetById(int id);

        Product GetByName(string name);

        Product GetByDescription(string description);
        Product GetByPrice(decimal price);
        Product GetByQuantity(int quantity);
        bool ProductExists(int id);



        Product CreateProduct(Product product);
        bool Save();


        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
