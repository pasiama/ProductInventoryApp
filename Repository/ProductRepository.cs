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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

    

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
        //public void Add(Product product)
        //{
        //  throw new NotImplementedException();
        //}

        //public void Delete(int id)
        //{
        //  throw new NotImplementedException();
        //}

        //public IEnumerable<Product> GetAll()
        //{
        //    return _products;
        //}

        //public Product GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        // public void Update(Product product)
        //{
        //   throw new NotImplementedException();
        //}
    } 
}
