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

        public async Task<IActionResult> AddToCart(int id)
        {
            ProductModel product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("CartItems") ?? new List<CartItemModel>();

            CartItemModel cartItem = cartItems.FirstOrDefault(item => item.ProductId == id);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItemModel(product) { Quantity = 1 });
            }

            HttpContext.Session.SetJson("CartItems", cartItems);
            //return RedirectToAction("Index");

            return Redirect(Request.Headers["Referer"].ToString());     //quay lại trang trước đó
        }
    }
}
