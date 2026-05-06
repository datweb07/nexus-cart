using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NexusCart.Repository.Components
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly DBContext _context;
        public BrandViewComponent(DBContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _context.Brands.ToListAsync();
            return View(brands);
        }
    }
}
