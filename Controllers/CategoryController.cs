using Microsoft.AspNetCore.Mvc;
using NexusCart.Models;
using NexusCart.Repository;
using Microsoft.EntityFrameworkCore;

namespace NexusCart.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DBContext _context;
        public CategoryController(DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string slug = "")
        {
            CategoryModel categoryModel = _context.Categories.Where(c => c.Slug == slug).FirstOrDefault();

            if (categoryModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var productByCategory = _context.Products.Where(p => p.CategoryId == categoryModel.Id);
            return View(await productByCategory.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
