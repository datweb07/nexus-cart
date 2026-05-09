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

            TempData["success"] = "Sản phẩm đã được thêm vào giỏ hàng!";

            return Redirect(Request.Headers["Referer"].ToString());     //quay lại trang trước đó
        }

        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("CartItems");

            CartItemModel cartItem = cartItems.Where(item => item.ProductId == id).FirstOrDefault();

            //if (cartItem != null)
            //{
            //    cartItem.Quantity--;
            //    if (cartItem.Quantity <= 0)
            //    {
            //        cartItems.Remove(cartItem);
            //    }
            //}

            //HttpContext.Session.SetJson("CartItems", cartItems);

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else
            {
                cartItems.RemoveAll(item => item.ProductId == id);
            }

            if (cartItems.Count == 0)
            {
                HttpContext.Session.Remove("CartItems");
            }
            else
            {
                HttpContext.Session.SetJson("CartItems", cartItems);
            }

            TempData["success"] = "Sản phẩm đã được giảm số lượng!";

            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Increase(int id)
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("CartItems");

            CartItemModel cartItem = cartItems.Where(item => item.ProductId == id).FirstOrDefault();

            //if (cartItem != null)
            //{
            //    cartItem.Quantity--;
            //    if (cartItem.Quantity <= 0)
            //    {
            //        cartItems.Remove(cartItem);
            //    }
            //}

            //HttpContext.Session.SetJson("CartItems", cartItems);

            if (cartItem.Quantity >= 1)
            {
                cartItem.Quantity++;
            }
            else
            {
                cartItems.RemoveAll(item => item.ProductId == id);
            }

            if (cartItems.Count == 0)
            {
                HttpContext.Session.Remove("CartItems");
            }
            else
            {
                HttpContext.Session.SetJson("CartItems", cartItems);
            }

            TempData["success"] = "Sản phẩm đã được tăng số lượng!";

            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("CartItems");

            cartItems.RemoveAll(item => item.ProductId == id);

            if (cartItems.Count == 0)
            {
                HttpContext.Session.Remove("CartItems");
            }
            else
            {
                HttpContext.Session.SetJson("CartItems", cartItems);
            }

            TempData["success"] = "Sản phẩm đã được xóa khỏi giỏ hàng!";

            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Clear(int id)
        {
            HttpContext.Session.Remove("CartItems");
            TempData["success"] = "Giỏ hàng đã được làm mới!";
            return RedirectToAction("Index", "Cart");
        }
    }
}
