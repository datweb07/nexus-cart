using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NexusCart.Models;
using NexusCart.Repository;

namespace NexusCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DBContext _context;
        public CategoryController (DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderByDescending(p => p.Id).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                categoryModel.Slug = categoryModel.Name?.ToLower().Replace(" ", "-");
                var slug = await _context.Categories.Where(p => p.Slug == categoryModel.Slug).FirstOrDefaultAsync();

                if (slug != null)
                {
                    ModelState.AddModelError("Name", "A category with the same name already exists.");
                    return View(categoryModel);
                }
                
                _context.Add(categoryModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Category creation failed";
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
            return View(categoryModel);
        }

        public async Task<IActionResult> Edit(int id)
        { 
            CategoryModel category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                categoryModel.Slug = categoryModel.Name?.ToLower().Replace(" ", "-");
                var slug = await _context.Categories.Where(p => p.Slug == categoryModel.Slug).FirstOrDefaultAsync();

                if (slug != null)
                {
                    ModelState.AddModelError("Name", "A category with the same name already exists.");
                    return View(categoryModel);
                }

                _context.Update(categoryModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Category update failed";
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
            return View(categoryModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            CategoryModel category = await _context.Categories.FindAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
