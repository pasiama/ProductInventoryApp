using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.DatabaseContext
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //  modelBuilder
        //    .Entity<Product>()
        //  .HasData(
        //    new Product
        //  {
        //    Id = 1,
        //  Name = "Product1",
        //Price = 10.0m,
        //Quantity = 100,
        //                      Description = "Description1"
        //                },
        //              new Product
        //            {
        //              Id = 2,
        //            Name = "Product2",
        //          Price = 20.0m,
        //        Quantity = 200,
        //      Description = "Description1"
        //},
        //              new Product
        //            {
        //              Id = 3,
        //            Name = "Product3",
        //          Price = 30.0m,
        //        Quantity = 300,
        //      Description = "Description1"
        //}
        //);
        //}



    }
}
