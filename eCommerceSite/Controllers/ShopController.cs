using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext DbContext { get; }
        public ShopController(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }


        public async Task<IActionResult> Products(string? id = null)
        {
            if (id == null) return View(await DbContext.Products.ToListAsync());
            Categories cat = Enum.Parse<Categories>(id);
            Console.WriteLine("Category selected: " + id);
            return View(await DbContext.Products.Where(p => p.Category == cat).ToListAsync());
        }

        public IActionResult ProfilePartial()
        {
            return PartialView("_Profile");
        }

    }
}
