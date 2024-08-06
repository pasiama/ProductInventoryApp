using Mapster;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.DatabaseContext;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly ApplicationContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ApplicationContext context, ILogger<ProductRepository>logger) {
        _context = context;
            _logger = logger;
        }
       
        public async Task<List<Product>>  GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return products;
            } 
            
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Product>();
            }
            
        }

        public async Task<Product> Add(Product product)
        {
            try {
                // Get the current highest Id
               // var maxId = await _context.Products.MaxAsync(p => (int?)p.InventoryId) ?? 0;
               // product.InventoryId = maxId + 1;

                var products = await _context.Products.AddAsync(product);
                _context.SaveChanges();
                return product;
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return new Product();
            }
         
          
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                var saved = await _context.SaveChangesAsync();
                return saved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }

        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }
        public async Task<Product> GetById(string Productid)
        {
            try 
            { 
                var product =  _context.Products.Where(p => p.Id == Productid).FirstOrDefault();
                return product; 
            } catch (Exception ex)
            {
           _logger.LogError($"Error {ex.Message}");
                return new Product();
            }
            
        }

        public bool ProductExists(string id)
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
                _logger.LogError(ex.Message);
                return new Product();
            }
        }


        public  bool  Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try {

                var existingProduct = _context.Products.Find(product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description ?? string.Empty;
                    existingProduct.Price = product.Price;
                    existingProduct.Quantity = product.Quantity;
                    existingProduct.UpdatedBy = product.UpdatedBy;
                    existingProduct.UpdatedAt = product.UpdatedAt;
                    
                    return Save();
                }
               _context.Products.Update(product);
                return false;
            }
            catch(Exception ex ) {
            _logger.LogError($"{ex.Message}");
                return false;
            }
           
        }

        public async Task<Product?> Delete(string id)
        {
            try {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return null;
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return product;
            } catch (Exception ex) {
            _logger.LogError(ex.Message);
                return null;
            }
           
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
