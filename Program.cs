using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexusCart.Models;
using NexusCart.Repository;

namespace NexusCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //cloudinary configuration
            //Account account = new Account(
            //builder.Configuration["CloudinarySettings:CloudName"],
            //builder.Configuration["CloudinarySettings:ApiKey"],
            //builder.Configuration["CloudinarySettings:ApiSecret"]
            //);

            //Cloudinary cloudinary = new Cloudinary(account);
            //builder.Services.AddSingleton(cloudinary);

            //configure database connection
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddIdentity<AppUserModel, IdentityRole>().AddEntityFrameworkStores<DBContext>().AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            var app = builder.Build();

            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "category",
                pattern: "/category/{Slug?}",
                defaults: new { controller = "Category", action = "Index" });

            app.MapControllerRoute(
                name: "brand",
                pattern: "/brand/{Slug?}",
                defaults: new { controller = "Brand", action = "Index" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            //seeding data
            //var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DBContext>();
            //DataSeeder.SeedData(context);
            app.Run();
        }
    }
}
