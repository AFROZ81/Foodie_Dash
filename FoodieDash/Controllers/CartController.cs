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
        public IActionResult Summary()
        {
            // Get the cart from session
            List<MenuItem> cart = HttpContext.Session.GetObject<List<MenuItem>>("MyCart") ?? new List<MenuItem>();

            // If empty, kick them out
            if (cart.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(string PickupName, string PhoneNumber, string Email)
        {
            // 1. Get Cart
            List<MenuItem> cart = HttpContext.Session.GetObject<List<MenuItem>>("MyCart") ?? new List<MenuItem>();

            if (cart != null)
            {
                // 2. Create Header (The Receipt)
                OrderHeader orderHeader = new OrderHeader
                {
                    PickupName = PickupName,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    OrderDate = DateTime.Now,
                    OrderTotal = cart.Sum(x => x.Price)
                };

                _context.OrderHeaders.Add(orderHeader);
                await _context.SaveChangesAsync(); // Save to get the generated ID

                // 3. Create Details (The Line Items)
                foreach (var item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderHeaderId = orderHeader.Id, // Link to the header above
                        MenuItemId = item.Id,
                        Name = item.Name,
                        Price = item.Price
                    };
                    _context.OrderDetails.Add(orderDetail);
                }

                await _context.SaveChangesAsync();

                // 4. Clear the Session (Cart is now empty)
                HttpContext.Session.Remove("MyCart");

                return RedirectToAction("OrderConfirmation", new { id = orderHeader.Id });
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id); // Simple view to say "Thank You"
        }
    }
}