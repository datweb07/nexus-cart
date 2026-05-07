using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusCart.Models;
using NexusCart.Repository;

namespace NexusCart.Controllers
{
    public class BrandController : Controller
    {
        private readonly DBContext _context;
        public BrandController(DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string slug = "")
        {
            BrandModel brandModel = _context.Brands.Where(b => b.Slug == slug).FirstOrDefault();

            if (brandModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var productByBrand = _context.Products.Where(p => p.BrandId == brandModel.Id);

            return View(await productByBrand.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
