using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusCart.Models;
using NexusCart.Repository;

namespace NexusCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly DBContext _context;
        public BrandController(DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.OrderByDescending(p => p.Id).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModel brandModel)
        {
            if (ModelState.IsValid)
            {
                brandModel.Slug = brandModel.Name?.ToLower().Replace(" ", "-");
                var slug = await _context.Brands.Where(p => p.Slug == brandModel.Slug).FirstOrDefaultAsync();

                if (slug != null)
                {
                    ModelState.AddModelError("Name", "A brand with the same name already exists.");
                    return View(brandModel);
                }

                _context.Add(brandModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Brand created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Brand creation failed";
                List<String> errors = new List<String>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("; ", errors);
                return BadRequest(errorMessage);
            }

            return View(brandModel); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            BrandModel brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandModel brandModel)
        {
            if (ModelState.IsValid)
            {
                brandModel.Slug = brandModel.Name?.ToLower().Replace(" ", "-");
                var slug = await _context.Brands.Where(p => p.Slug == brandModel.Slug).FirstOrDefaultAsync();

                if (slug != null)
                {
                    ModelState.AddModelError("Name", "A brand with the same name already exists.");
                    return View(brandModel);
                }

                _context.Update(brandModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Brand updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Brand update failed";
                List<String> errors = new List<String>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("; ", errors);
                return BadRequest(errorMessage);
            }
            return View(brandModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            BrandModel brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            TempData["success"] = "Brand deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
