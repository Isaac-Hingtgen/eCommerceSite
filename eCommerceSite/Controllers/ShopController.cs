using eCommerceSite.Data;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index(int? id = null)
        {
            if (id == null) return View();
            Categories cat = (Categories)id;
            // TODO: only get models of category
            Console.WriteLine("Category selected");
            return View();
        }

    }
}
