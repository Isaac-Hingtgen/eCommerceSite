using eCommerceSite.Data;
using eCommerceSite.Models;
using eCommerceSite.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;

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

        [HttpPost]
        public async Task<IActionResult> Login(string json)
        {
            JsonNode? jsondata = JsonNode.Parse(json);

            if (jsondata is null) { return Json(new { status = "false", message = "null data" }); }

            string? username = (string?) jsondata["username"];
            string? password = (string?) jsondata["password"];

            if (username is null || password is null) { return Json(new { status = "false", message = "null data" }); }

            Profile? user = await DbContext.Profiles.SingleOrDefaultAsync(p => p.Name.Equals(username));

            if (user is null || !user.IsAuthenticated(password))
            {
                return Json(new { status = "false", message = "Invalid credentials" });
            }

            return Json(new { status = "true" });
        }

        [HttpPost]
        public async Task<IActionResult> Register(string json)
        {
            JsonNode? jsondata = JsonNode.Parse(json);

            if (jsondata is null) { return Json(new { status = "false", message = "null data" }); }

            string? username = (string?) jsondata["username"];
            string? password = (string?) jsondata["password"];

            if (username is null || password is null) { return Json(new { status = "false", message = "null data" }); }

            if (await DbContext.Profiles.SingleOrDefaultAsync(p => p.Name.Equals(username)) is not null)
            {
                return Json(new { status = "false", message = "Username unavailable" });
            }

            Profile user = new Profile(username, password);
            try
            {
                await DbContext.AddAsync(user);
                await DbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                return Json(new { status = "false", message = "Could not add user, try again" });
            }

            return Json(new { status = "true" });
        }

        public IActionResult CartPartial()
        {
            return PartialView("_Cart");
        }
    }
}
