using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.Constants;
using ProductInventoryApp.DatabaseContext;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;
using ProductInventoryApp.Repository;
using ProductInventoryApp.Services.Interfaces;

namespace ProductInventoryApp.Services.Providers
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductServices> _logger;

        public ProductServices(IProductRepository productRepository, ILogger<ProductServices>logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Product> Add(ProductRequestDto productDto)
        {
            try
            {
              var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description ?? string.Empty,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                    CreatedBy = productDto.CreatedBy,
                    Total = productDto.TotalAmount,
                   };
                await _productRepository.Add(product);
                var results = product.Adapt<Product>();
                return results;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Product();
            }
        }

        //database normalization

        public async Task<Product?> CreateProduct(ProductRequestDto productDto)
        {
            try 
            {
                if (productDto == null)
                {
                    _logger.LogInformation("Something bad happened. fill all information and try again");
                    return null;
                }
                if (productDto.Name == null)
                {
                    _logger.LogInformation("Product name is required ");
                    return null;
                }
                    var products = new Product
                {
                   
                    Name = productDto.Name,
                    Description = productDto.Description ?? string.Empty,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                    CreatedBy = Auth.GetUser(),
                    Total = productDto.TotalAmount,
                };

                await _productRepository.Add(products);
                var count = await _productRepository.SaveChanges();

                if (count > 0)
                {
                    _logger.LogInformation("Could not creat an inventory");
                    return products;
                }

                return new Product();
            } 
                catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw;
            }
           
        }

        public async Task<Product?> GetProduct(string id)
        {
            try 
            {
                var product = await _productRepository.GetById(id);
                if (_productRepository.GetProducts() == null)
                {
                    _logger.LogInformation("There are no products to be displayed");
                    return null;
                }
                    return product;
            }
                catch (Exception ex) 
            { 
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            try {
                var products = await _productRepository.GetProducts();
                return products;
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public bool ProductExists(string id)
        {
            var product = _productRepository.ProductExists(id);
            return product;
        }

        public async Task<bool> UpdateProduct(string id, ProductRequestDto productDto)
        {
            try 
            {
                if (productDto == null)
                {
                    return false;
                }

                if (_productRepository.GetProducts() == null)
                {
                    return false;
                }
                    var product = new Product
                {
                    Id = id,
                    Name = productDto.Name,
                    Description = productDto.Description ?? string.Empty,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                    UpdatedBy = productDto.UpdatedBy
                 };
                 var results = _productRepository.UpdateProduct(product);
                 var count = await _productRepository.SaveChanges();

                if (count > 0)
                {
                    return await results;
                }

                return false;

            } catch (Exception ex) 
            {
                _logger.LogError(ex.Message);   
                throw;
            }
        }

        public async Task<Product> DeleteProduct(string id)
        {
            try {
                if (_productRepository.GetById(id) == null)
                {
                    _logger.LogInformation("Item can not be found");
                    return null;
                }
                    var results = await _productRepository.Delete(id);
                    return results;

                } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
    
