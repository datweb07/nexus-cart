using Microsoft.EntityFrameworkCore;
using NexusCart.Models;

namespace NexusCart.Repository
{
    public class DataSeeder
    {
        public static void SeedData(DBContext context)
        {
            //if (!context.Categories.Any())
            //{
            //    context.Categories.AddRange(
            //        new Models.CategoryModel { Name = "Electronics", Description = "Electronic gadgets and devices", Slug = "electronics", Status = 1 },
            //        new Models.CategoryModel { Name = "Books", Description = "All kinds of books", Slug = "books", Status = 1 },
            //        new Models.CategoryModel { Name = "Clothing", Description = "Men and Women Clothing", Slug = "clothing", Status = 1 }
            //    );
            //    context.SaveChanges();
            //}
            context.Database.Migrate();

            if (!context.Products.Any()) 
            {
                CategoryModel phone = new CategoryModel { Name = "Apple", Description = "Apple device", Slug = "apple", Status = 1 };
                CategoryModel pc = new CategoryModel { Name = "Samsung", Description = "Samsung device", Slug = "samsung", Status = 1 };
                BrandModel dell = new BrandModel { Name = "Dell", Description = "Dell device", Slug = "dell", Status = 1 };

                context.Products.AddRange(
                    new ProductModel { Name = "iPhone 13", Description = "Latest Apple iPhone", Slug = "iphone-13", Price = 999.99m, Category = phone, Brand = dell, ImageUrl ="abc.jpg" },
                    new ProductModel { Name = "Samsung Galaxy S21", Description = "Latest Samsung Galaxy", Slug = "samsung-galaxy-s21", Price = 899.99m, Category = pc, Brand = dell, ImageUrl="abc.jpg" }
                );

                context.SaveChanges();
            }
        }
    }
}
