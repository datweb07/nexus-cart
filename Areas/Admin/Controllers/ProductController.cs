using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
