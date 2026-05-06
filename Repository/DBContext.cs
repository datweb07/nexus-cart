using Microsoft.EntityFrameworkCore;

namespace NexusCart.Repository
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Models.BrandModel> Brands { get; set; } 
        public DbSet<Models.CategoryModel> Categories { get; set; }
        public DbSet<Models.ProductModel> Products { get; set; }
    }
}
