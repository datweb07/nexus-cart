using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NexusCart.Models;
using NexusCart.Repository;

namespace NexusCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(DBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.OrderByDescending(p => p.Id).Include(p => p.Category).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel productModel)
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", productModel.CategoryId);
            ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", productModel.BrandId);

            if (ModelState.IsValid)
            {
                productModel.Slug = productModel.Name?.ToLower().Replace(" ", "-");
                var slug = await _context.Products.Where(p => p.Slug == productModel.Slug).FirstOrDefaultAsync();

                if (slug != null)
                {
                    ModelState.AddModelError("Name", "A product with the same name already exists.");
                    return View(productModel);
                }
                else
                {
                    if (productModel.ImageFile != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                        string fileName = Guid.NewGuid().ToString() + "_" + productModel.ImageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await productModel.ImageFile.CopyToAsync(fileStream);
                            fileStream.Close();
                        }
                        productModel.ImageUrl = fileName;
                    }
                }
                _context.Add(productModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Product creation failed";
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
            //if (!ModelState.IsValid)
            //{
            //    return View(productModel);
            //}

            //_context.Products.Add(productModel);
            //await _context.SaveChangesAsync();

            //TempData["success"] = "Product created successfully";

            //return RedirectToAction(nameof(Index));
            return View(productModel);
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
        //    ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", product.BrandId);
        //    return View(product);
        //}
    }
}
