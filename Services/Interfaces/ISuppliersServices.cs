using ProductInventoryApp.DTO;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Services.Interfaces
{
    public interface ISuppliersServices
    {
        public Task<Suppliers> GetSupplier(string id);

        public Task<Suppliers> DeleteSupplier(string id);

        public bool SupplierExists(string id);
        public Task<bool> UpdateSupplier(string id, SuppliersDto suppliersDto);

        public Task<Suppliers> Add(SuppliersDto suppliersDto);

        public Task<Suppliers> CreateSupplier(SuppliersDto suppliersDto);

        public Task<List<Suppliers>> GetSuppliers();
    }
}
