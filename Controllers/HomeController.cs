using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusCart.Models;
using NexusCart.Repository;
using System.Diagnostics;

namespace NexusCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(DBContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include("Brand").Include("Category").ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
