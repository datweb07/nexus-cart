using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NexusCart.Models;
using NexusCart.Repository;

namespace NexusCart.Controllers
{
    public class ProductController : Controller
    {
        private readonly DBContext _context;
        public ProductController(DBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var productById = _context.Products.Where(p => p.Id == id).FirstOrDefault();

            return View(productById);
        }
    }
}
