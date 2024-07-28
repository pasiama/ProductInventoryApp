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

        public async Task<List<Product>>  GetProducts()
        {
            try
            {
                return _context.Products.OrderBy(p => p.Id).ToList();
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Product>();
            }
            
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
        public async Task<Product> GetById(int Productid)
        {
            try 
            {
                return  _context.Products.Where(p => p.Id == Productid).FirstOrDefault();
            } catch (Exception ex)
            {
            Console.WriteLine(ex.Message);
                return new Product();
            }
            
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public async Task<Product> GetByName(string Productname)
        {
            try
            {
                return _context.Products.Where(p => p.Name == Productname).FirstOrDefault();
            } catch (Exception ex) 
            {
            Console.WriteLine(ex.Message);
                return new Product();
            }
           
        }

        public async Task<Product> GetByDescription(string Productdescription)
        {
            try 
            {
                return _context.Products.Where(p => p.Description == Productdescription).FirstOrDefault();
            }
            catch(Exception ex)
            {
            Console.WriteLine(ex.Message);
                return new Product();
            }
            
        }

        public async Task<Product> GetByPrice(decimal Productprice)
        {
            try
            {
                return _context.Products.Where(p => p.Price == Productprice).FirstOrDefault();
            } catch (Exception ex) 
            {
            Console.WriteLine(ex.Message);
                return new Product();
            }
            
        }

        public async Task<Product> GetByQuantity(int Productquantity)
        {
            try 
            {
                return _context.Products.Where(p => p.Quantity == Productquantity).FirstOrDefault();
            } catch (Exception ex) {
            Console.WriteLine(ex.Message);
                return new Product();
            }
            
        }

        public async Task<Product> DeleteProduct(Product product)
        {
            try
            {

               _context.Products.Remove(product);
                _context.SaveChanges();
                return product;
            } catch (Exception ex) {
               Console.WriteLine(ex.Message);
                return new Product();
            }
        }



        public  bool  Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        Product IProductRepository.CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return Save();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

       



        Product IProductRepository.GetByName(string name)
        {
            throw new NotImplementedException();
        }

        Product IProductRepository.GetByDescription(string description)
        {
            throw new NotImplementedException();
        }

        Product IProductRepository.GetByPrice(decimal price)
        {
            throw new NotImplementedException();
        }

        Product IProductRepository.GetByQuantity(int quantity)
        {
            throw new NotImplementedException();
        }

      
    } 
}
