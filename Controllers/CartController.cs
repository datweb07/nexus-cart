using Microsoft.AspNetCore.Mvc;
using NexusCart.Models;
using NexusCart.Models.ViewModels;
using NexusCart.Repository;

namespace NexusCart.Controllers
{
    public class CartController : Controller
    {
        private readonly DBContext _context;

        public CartController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("CartItems") ?? new List<CartItemModel>();
            CartItemViewModel cartItemViewModel = new()
            {
                cartItemModels = cartItems,
                GrandTotal = cartItems.Sum(item => item.TotalPrice)
            };
            return View(cartItemViewModel);
        }

        public IActionResult Checkout()
        {
            return View("~/Views/Checkout/Index.cshtml");
        }
    }
}
