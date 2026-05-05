using Microsoft.AspNetCore.Mvc;

namespace NexusCart.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
