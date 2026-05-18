using Microsoft.AspNetCore.Mvc;
using NexusCart.Models;
using NexusCart.Repository;
using System.Security.Claims;

namespace NexusCart.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DBContext _context;
        public CheckoutController (DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var oderCode = Guid.NewGuid().ToString();
                var order = new OrderModel();
                order.OrderCode = oderCode;
                order.UserName = userEmail;
                order.OrderStatus = 1;
                order.CreatedDate = DateTime.Now;
                _context.Orders.Add(order);
                _context.SaveChanges();

                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("CartItems") ?? new List<CartItemModel>();
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetailsModel();
                    orderDetail.UserName = userEmail;
                    orderDetail.OrderCode = oderCode;
                    orderDetail.ProductId = item.ProductId;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Price = item.Price;

                    _context.OrderDetails.Add(orderDetail);
                    _context.SaveChanges();
                }
                HttpContext.Session.Remove("CartItems");
                TempData["SuccessMessage"] = "Order placed successfully!";

                return RedirectToAction("Index", "Cart");
            }
            return View();
        }
    }
}
