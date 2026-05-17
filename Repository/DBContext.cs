using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusCart.Models;

namespace NexusCart.Repository
{
    public class DBContext : IdentityDbContext<AppUserModel>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Models.BrandModel> Brands { get; set; } 
        public DbSet<Models.CategoryModel> Categories { get; set; }
        public DbSet<Models.ProductModel> Products { get; set; }
        public DbSet<Models.OrderModel> Orders { get; set; }
        public DbSet<Models.OrderDetailsModel> OrderDetails { get; set; }
    }
}
