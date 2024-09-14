using coreWebAPIVendorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace coreWebAPIVendorProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
