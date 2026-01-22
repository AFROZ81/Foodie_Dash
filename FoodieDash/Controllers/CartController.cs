using FoodieDash.Data;
using FoodieDash.Models;
using FoodieDash.Utility; // Import the helper we just made
using Microsoft.AspNetCore.Mvc;

namespace FoodieDash.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 1. Get the current list of items from the Session
            List<MenuItem> cart = HttpContext.Session.GetObject<List<MenuItem>>("MyCart") ?? new List<MenuItem>();

            // 2. Pass it to the view
            return View(cart);
        }

        public IActionResult Add(int id)
        {
            // 1. Find the product in the database
            MenuItem? itemToAdd = _context.MenuItems.Find(id);

            if (itemToAdd != null)
            {
                // 2. Get the existing cart (or create a new one if empty)
                List<MenuItem> cart = HttpContext.Session.GetObject<List<MenuItem>>("MyCart") ?? new List<MenuItem>();

                // 3. Add the new item
                cart.Add(itemToAdd);

                // 4. Save the updated list back to Session
                HttpContext.Session.SetObject("MyCart", cart);
            }

            // 5. Go back to Home Page
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Remove(int id)
        {
            // 1. Get the current cart
            List<MenuItem> cart = HttpContext.Session.GetObject<List<MenuItem>>("MyCart") ?? new List<MenuItem>();

            // 2. Find the item
            var itemToRemove = cart.FirstOrDefault(i => i.Id == id);

            // 3. Remove it
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("MyCart", cart);
            }

            // 4. Reload the page
            return RedirectToAction(nameof(Index));
        }
    }
}