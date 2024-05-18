using eCommerceSite.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products {  get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}
