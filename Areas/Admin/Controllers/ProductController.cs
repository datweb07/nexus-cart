using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusCart.Repository;

namespace NexusCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DBContext _context;
        public ProductController(DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.OrderByDescending(p => p.Id).Include(p => p.Category).ToListAsync());
        }
    }
}
