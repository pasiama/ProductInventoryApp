using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.DatabaseContext;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Repository
{
    public class SuppliersRepository: ISuppliersRepository
    {
        private readonly List<Suppliers> suppliers = new List<Suppliers>();
        private readonly ApplicationContext _context;
        private readonly ILogger<SuppliersRepository> _logger;
        public SuppliersRepository(ApplicationContext context, ILogger<SuppliersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }
        public async Task<Suppliers> Add(Suppliers supplier)
        {
            try
            {
                var addsupplier = await _context.Suppliers.AddAsync(supplier);
                _context.SaveChanges();
                return supplier;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return new Suppliers();
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

        public bool CreateSupplier(Suppliers supplier)
        {
            _context.Add(supplier);
            return Save();

        }


        public async Task<bool> UpdateSupplier(Suppliers supplier)
        {
            try
            {

                var updatesupplier = _context.Suppliers.Find(supplier.Id);
                if (updatesupplier != null)
                {
                    updatesupplier.Name = supplier.Name;
                    updatesupplier.Price = supplier.Price;
                    updatesupplier.Category = supplier.Category;
                    updatesupplier.SupplierId = supplier.SupplierId;
                    updatesupplier.Quantity = supplier.Quantity;
                    updatesupplier.Contact = supplier.Contact;
                    updatesupplier.ImageUrl = supplier.ImageUrl;
                    updatesupplier.SupplyType = supplier.SupplyType;
                    updatesupplier.Product = supplier.Product;

                    return Save();

                }
                _context.Suppliers.Update(supplier);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;

            }

        }

        public async Task<Suppliers> DeleteSupplier(Suppliers supplier)
        {
            try
            {
                var deletesupplier = _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Suppliers();
            }

        }

        public async Task<Suppliers?> Delete(string id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return new Suppliers();
                }

                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();

                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Suppliers();
            }

        }

        public IQueryable<Suppliers> GetSuppliers()
        {
            try
            {
                var supplier = _context.Suppliers.AsNoTracking().AsQueryable();
                return supplier.AsQueryable();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Suppliers>().AsQueryable();
            }
        }

        public async Task<Suppliers> GetSupplierById(string supplierId)
        {
            try
            {
                var supplier = _context.Suppliers.Where(p => p.Id == supplierId).FirstOrDefault();
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message}");
                return new Suppliers();
            }
        }

    }
}



