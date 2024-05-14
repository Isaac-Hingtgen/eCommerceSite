using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products {  get; set; }

    }
}
