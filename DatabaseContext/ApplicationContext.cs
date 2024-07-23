using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.DatabaseContext
{
    public class ApplicationContext : DbContext
    {
       public ApplicationContext(DbContextOptions<ApplicationContext>options) :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("DefaultConnection");
        }
    }
}
