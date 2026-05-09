using Microsoft.EntityFrameworkCore;
using NexusCart.Repository;
using CloudinaryDotNet;

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

            var app = builder.Build();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

            //seeding data
            //var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DBContext>();
            //DataSeeder.SeedData(context);
            app.Run();
        }
    }
}
