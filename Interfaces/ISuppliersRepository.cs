using ProductInventoryApp.Models;

namespace ProductInventoryApp.Interfaces
{
    public interface ISuppliersRepository
    {
        public IQueryable<Suppliers> GetSuppliers();

        public Task<Suppliers> GetSupplierById(string supplierId);

        public bool CreateSupplier(Suppliers supplier);
        public bool Save();

       // public Task<int> TotalProductCount();
        public Task<bool> UpdateSupplier(Suppliers supplier);

        public Task<Suppliers> DeleteSupplier(Suppliers supplier);


        public Task<Suppliers> Add(Suppliers supplier);

        public Task<int> SaveChanges();

        Task<Suppliers> Delete(string id);
    }
}
