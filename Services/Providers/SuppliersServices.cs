using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.Constants;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;
using ProductInventoryApp.Repository;
using ProductInventoryApp.Services.Interfaces;

namespace ProductInventoryApp.Services.Providers
{
    public class SuppliersServices : ISuppliersServices
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly ILogger<SuppliersServices> _logger;
        public SuppliersServices(ISuppliersRepository suppliersRepository, ILogger<SuppliersServices> logger)
        {
            _logger = logger;
        _suppliersRepository = suppliersRepository;
        }

        public async Task<Suppliers> Add(SuppliersDto suppliersDto)
        {
            try
            {
                var supplier = new Suppliers
                {
                    Name = suppliersDto.Name,
                    
                    Price = suppliersDto.Price,
                    Quantity = suppliersDto.Quantity,
                    Product = suppliersDto.Product,
                     Contact = suppliersDto.Contact,
                    Category = suppliersDto.Category,
                    ImageUrl = suppliersDto.ImageUrl,
                    SupplyType = suppliersDto.SupplyType,
                    SupplierId = suppliersDto.SupplierId

                };
                await _suppliersRepository.Add(supplier);
                var results = supplier.Adapt<Suppliers>();
                return results;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Suppliers();
            }
        }

        public async Task<Suppliers> CreateSupplier(SuppliersDto suppliersDto)
        {
            try
            {
                if (suppliersDto == null)
                {
                    _logger.LogInformation("Something bad happened. fill all information and try again");
                    return new Suppliers();
                }
                if (suppliersDto.Name == null)
                {
                    _logger.LogInformation("Product name is required ");
                    return new Suppliers();
                }
                var supplier = new Suppliers
                {
                    Name = suppliersDto.Name,

                    Price = suppliersDto.Price,
                    Quantity = suppliersDto.Quantity,
                    Product = suppliersDto.Product,
                    Contact = suppliersDto.Contact,
                    Category = suppliersDto.Category,
                    ImageUrl = suppliersDto.ImageUrl,
                    SupplyType = suppliersDto.SupplyType,
                    SupplierId = suppliersDto.SupplierId

                };

                await _suppliersRepository.Add(supplier);
                var count = await _suppliersRepository.SaveChanges();

                if (count > 0)
                {
                    _logger.LogInformation("Could not creat an inventory");
                    return supplier;
                }

                return new Suppliers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Suppliers();
            }

        }

        public async Task<Suppliers> DeleteSupplier(string id)
        {
            try
            {
                if (_suppliersRepository.GetSupplierById(id) == null)
                {
                    _logger.LogInformation("Item can not be found");
                    return new Suppliers();
                }
                var results = await _suppliersRepository.Delete(id);
                return results;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Suppliers();
            }
        }

        public async Task<Suppliers> GetSupplier(string id)
        {
            try
            {
                var supplier = await _suppliersRepository.GetSupplierById(id);
                if (_suppliersRepository.GetSuppliers() == null)
                {
                    _logger.LogInformation("There are no products to be displayed");
                    return new Suppliers();
                }
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Suppliers();
            }
        }

        public bool SupplierExists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateSupplier(string id, SuppliersDto suppliersDto)
        {
            try
            {
                if (suppliersDto == null)
                {
                    return false;
                }

                if (_suppliersRepository.GetSuppliers() == null)
                {
                    return false;
                }
                var supplier = new Suppliers
                {
                    Name = suppliersDto.Name,

                    Price = suppliersDto.Price,
                    Quantity = suppliersDto.Quantity,
                    Product = suppliersDto.Product,
                    Contact = suppliersDto.Contact,
                    Category = suppliersDto.Category,
                    ImageUrl = suppliersDto.ImageUrl,
                    SupplyType = suppliersDto.SupplyType,
                    SupplierId = suppliersDto.SupplierId

                };
                var results = _suppliersRepository.UpdateSupplier(supplier);
                var count = await _suppliersRepository.SaveChanges();

                if (count > 0)
                {
                    return await results;
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Suppliers>> GetSuppliers()
        {
            try
            {
                var supplier =   _suppliersRepository.GetSuppliers();

                return supplier.ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Suppliers>();
            }
        }

   
    }
}
