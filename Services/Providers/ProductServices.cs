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
using System.Linq.Expressions;

namespace ProductInventoryApp.Services.Providers
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductServices> _logger;

        public ProductServices(IProductRepository productRepository, ILogger<ProductServices> logger)
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
                    CreatedBy = Auth.GetUser(),

                    Category = productDto.Category,
                    ProductUrl = productDto.ProductUrl,
                    Availability = productDto.Availability,
                    Total = productDto.TotalAmount,
                    ProductVat = productDto.Vat,
                    ProductProfit = productDto.Profit,
                    ProductUnitSellingPrice = productDto.UnitSellingPrice,

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

        public async Task<int> GetTotalProductCount()
        {
            try {
                var totalProducts = await _productRepository.TotalProductCount();
                return totalProducts;
            }
            catch (Exception ex) {
                _logger.LogError($"{ex.Message}");
                return 0;
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
                   
                    Category = productDto.Category,
                    ProductUrl = productDto.ProductUrl ?? string.Empty,
                    Availability = productDto.Availability,
                    Total = productDto.TotalAmount,
                    ProductVat = productDto.Vat,
                    ProductProfit = productDto.Profit,
                    ProductUnitSellingPrice = productDto.UnitSellingPrice,
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

        public async Task<ProductResponseDto> GetProducts()
        {
            try {
                var products = _productRepository.GetProducts();
                var totalProductCount = products.Count();
                var overallTotalAmount = products.Sum(p => p.Total);

                var totalProfits = products.Sum(p => p.ProductProfit);
                var totalVat = products.Sum(p => p.ProductVat);

                var productList = await products.ToListAsync();

                var results = new ProductResponseDto
                {
                    OverallTotalAmount = overallTotalAmount,
                    TotalProductCount = totalProductCount,
                    TotalProfits = totalProfits,
                    TotalVat = totalVat,
                    Products = productList,
                };

                    

                return results;

            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return new ProductResponseDto();
            }
        }

       

        public async Task<ProductResponseDto> GetPaginationProducts(int page, int pageSize)
        {
            try
            {
                // Apply pagination
                var products = await _productRepository.GetAllProductWithPagination(page, pageSize) ?? Enumerable.Empty<Product>() ;

                var totalProductCount = await _productRepository.GetAllProductWithPagination(page, pageSize) ?? Enumerable.Empty<Product>();
                var overallTotalAmount =  products.Sum(p => p.Total);
                var totalProfits =  products.Sum(p => p.ProductProfit);
                var totalVat =  products.Sum(p => p.ProductVat);

                var productList =  products;

                var results = new ProductResponseDto
                {
                    OverallTotalAmount = overallTotalAmount,
                    TotalProductCount = totalProductCount.Count(),
                    TotalProfits = totalProfits,
                    TotalVat = totalVat,
                    Products = (List<Product>)productList,
                };

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ProductResponseDto();
            }
        }

        




        public class ProductResponseDto
        {
            public List<Product> Products { get; set; } = new List<Product>();
            public int TotalProductCount { get; set; }
            public decimal OverallTotalAmount { get; set; }
            public decimal TotalProfits {get; set;}
            public decimal TotalVat{ get; set;}

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
                        //UpdatedBy = productDto.UpdatedBy,
                    UpdatedBy = Auth.GetUser(),

                        Category = productDto.Category,
                        ProductUrl = productDto.ProductUrl,
                        Availability = productDto.Availability,
                        Total = productDto.TotalAmount,
                        ProductVat = productDto.Vat,
                        ProductProfit = productDto.Profit,
                        ProductUnitSellingPrice = productDto.UnitSellingPrice,
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
    
