using ProductInventoryApp.DatabaseContext;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context) {
        _context = context;
        }

        public ICollection<Product> GetProducts()
        {

            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }
        public Product GetById(int Productid)
        {
            return _context.Products.Where(p => p.Id == Productid).FirstOrDefault();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetByName(string Productname)
        {
            return _context.Products.Where(p => p.Name == Productname).FirstOrDefault();
        }

        public Product GetByDescription(string Productdescription)
        {
            return _context.Products.Where(p => p.Description == Productdescription).FirstOrDefault();
        }

        public Product GetByPrice(decimal Productprice)
        {
            return _context.Products.Where(p => p.Price == Productprice).FirstOrDefault();
        }

        public Product GetByQuantity(int Productquantity)
        {
            return _context.Products.Where(p => p.Quantity == Productquantity).FirstOrDefault();
        }

       

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        Product IProductRepository.CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    } 
}
