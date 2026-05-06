using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NexusCart.Repository.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly DBContext _context;
        public CategoryViewComponent(DBContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
    }
}
