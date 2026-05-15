using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NexusCart.Models;

namespace NexusCart.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;
        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                AppUserModel appUser = new AppUserModel
                {
                    UserName = user.Email,
                    Email = user.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(appUser);

                if (result.Succeeded)
                {
                    TempData["success"] = "Registration successful!";   
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        } 
    }
}
