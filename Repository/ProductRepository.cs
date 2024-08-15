using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.Common;
using ProductInventoryApp.Constants;
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
       
        public IQueryable <Product>  GetProducts()
        {
            try
            {
               var products =  _context.Products.AsNoTracking().AsQueryable();
                return products.AsQueryable();
            } 
            
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Product>().AsQueryable();
            }
            
        }

        public IQueryable<Product> GetPaginationProducts()
        {
            try
            {
                var products = _context.Products.AsNoTracking().AsQueryable();
                return products.AsQueryable();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Product>().AsQueryable();
            }

        }

        public async Task<PaginatedList<Product>> GetAllProductWithPagination(int page, int pageSize, string searchTerm)
        {
            try {
                IQueryable<Product> query = _context.Products.AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}") );
                }

                var Results = await PaginatedList<Product>.ToPagedList( query, page, pageSize );
            return Results;

            } catch (Exception ex) {
            _logger.LogError(ex.Message, ex);
                return null;
            }
        }


        public async Task<int> TotalProductCount() => await _context.Products.CountAsync();

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
                    existingProduct.UpdatedBy = Auth.GetUser();
                    existingProduct.UpdatedAt = DateTime.Now;


                    existingProduct.Category = product.Category;
                    existingProduct.ProductUrl = product.ProductUrl ?? string.Empty;
                    existingProduct.Availability = product.Availability;
                    existingProduct.Total = product.TotalAmount;
                    existingProduct.ProductVat = product.Vat;
                    existingProduct.ProductProfit = product.Profit;
                    existingProduct.ProductUnitSellingPrice = product.UnitSellingPrice;
                    
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
                    return new Product();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return product;
            } catch (Exception ex) {
            _logger.LogError(ex.Message);
                return new Product();
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
