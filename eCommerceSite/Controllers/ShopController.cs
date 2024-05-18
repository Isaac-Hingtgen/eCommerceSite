using eCommerceSite.Data;
using eCommerceSite.Models;
using eCommerceSite.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
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
            System.Diagnostics.Debug.WriteLine("entrace: Products");

            if (id == null) return View(await DbContext.Products.ToListAsync());
            Categories cat = Enum.Parse<Categories>(id);
            Console.WriteLine("Category selected: " + id);
            return View(await DbContext.Products.Where(p => p.Category == cat).ToListAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(string json)
        {
            JsonNode? jsondata = JsonNode.Parse(json);
            if (jsondata is null) { return Json(new { status = "false", message = "null data" }); }

            int? productId = (int?)jsondata["productId"];
            if (productId is null) { return Json(new { status = "false", message = "null data" }); }

            Cart cart = await GetCart();

            cart.Remove((int) productId);
            await DbContext.SaveChangesAsync();

            return Json(new { status = "true" });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string json)
        {
            System.Diagnostics.Debug.WriteLine("entrance: AddToCart");


            JsonNode? jsondata = JsonNode.Parse(json);
            if (jsondata is null) { return Json(new { status = "false", message = "null data" }); }

            int? productId = (int?)jsondata["productId"];
            if (productId is null) { return Json(new { status = "false", message = "null data" }); }

            Product? product = await DbContext.Products.SingleOrDefaultAsync(p => p.Id == (productId));
            if (product is null ) // TODO: add quantities
            {
                return Json(new { status = "false", message = "Product not available" });
            }
            
            Cart cart = await GetCart();
            System.Diagnostics.Debug.WriteLine(Request.Cookies["cart-id"]);

            cart.Add(product);
            await DbContext.SaveChangesAsync();

            return Json(new { status = "true" });
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

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Set additional authentication properties if needed
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Json(new { status = "true" });
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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

        public async Task<IActionResult> CartPartial()
        {
            Cart cart = await GetCart();
            System.Diagnostics.Debug.WriteLine("CART-CONTENTS: " + cart.Products.ToString());
            return PartialView("_Cart", cart);
        }


        private async Task<Cart> GetCart()
        {
            int? profileId = (User.Identity is not null && User.Identity.IsAuthenticated) 
                ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) 
                : null;
            string? cartCookie = Request.Cookies["cart-id"];
            System.Diagnostics.Debug.WriteLine("cart-id: " + cartCookie);
            Cart? cart = null;
            Profile? user = null;

            if (cartCookie is not null)
            {
                cart = await DbContext.Carts.Where(c => c.Id.Equals(cartCookie))
                    .Include(c => c.Products).FirstOrDefaultAsync();
            }

            if (profileId is not null)
            {
                user = await DbContext.Profiles
                    .Where(p => p.Id == profileId)
                    .Include(p => p.ShoppingCart)
                    .ThenInclude(c => c.Products)
                    .FirstOrDefaultAsync();
                if (user is not null)
                {
                    if (user.ShoppingCart is null)
                    {
                        user.ShoppingCart = new(user);
                    }
                    user.CartId = user.ShoppingCart.Id;
                    if (!user.CartId.Equals(cartCookie))
                    {
                        user.ShoppingCart.Add(cart);
                        CookieOptions options = new();
                        options.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Append("cart-id", user.CartId, options);
                        if (cart != null) DbContext.Carts.Remove(cart);
                    }
                    cart = user.ShoppingCart;
                }
            }

            if (cart is null)
            {
                cart = new();
                await DbContext.Carts.AddAsync(cart);
                CookieOptions options = new();
                options.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Append("cart-id", cart.Id, options);
            }

            await DbContext.SaveChangesAsync();

            System.Diagnostics.Debug.WriteLine("cart.Id: " + cart.Id);

            return cart;
        }
    }
}
