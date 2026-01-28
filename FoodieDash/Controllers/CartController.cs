using FoodieDash.Data;
using FoodieDash.Models;
using FoodieDash.Utility;
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
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("MyCart") ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult Add(int id)
        {
            MenuItem? itemToAdd = _context.MenuItems.Find(id);
            if (itemToAdd == null) return NotFound();

            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("MyCart") ?? new List<CartItem>();

            // Check if item already exists in cart
            var existingItem = cart.FirstOrDefault(c => c.MenuItem.Id == id);

            if (existingItem != null)
            {
                // Item exists, just increase quantity
                existingItem.Quantity++;
            }
            else
            {
                // Item is new, add it with Qty 1
                cart.Add(new CartItem { MenuItem = itemToAdd, Quantity = 1 });
            }

            HttpContext.Session.SetObject("MyCart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("MyCart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(c => c.MenuItem.Id == id);

            if (existingItem != null)
            {
                if (existingItem.Quantity > 1)
                {
                    existingItem.Quantity--;
                }
                else
                {
                    cart.Remove(existingItem);
                }
            }
            HttpContext.Session.SetObject("MyCart", cart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("MyCart") ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(c => c.MenuItem?.Id == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }
            HttpContext.Session.SetObject("MyCart", cart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("MyCart") ?? new List<CartItem>();

            if (cart.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // UPDATED: Now accepts 'Address' and 'paymentType' from the form
        public async Task<IActionResult> PlaceOrder(string PickupName, string PhoneNumber, string Address, string paymentType)
        {
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("MyCart") ?? new List<CartItem>();

            if (cart != null)
            {
                // Create Header
                OrderHeader orderHeader = new OrderHeader
                {
                    PickupName = PickupName,
                    PhoneNumber = PhoneNumber,
                    // UPDATED: Mapping the Address field
                    // Make sure your OrderHeader Model has an 'Address' property!
                    Address = Address,
                    // Optional: You can store the payment type if you added a property for it
                    // PaymentStatus = paymentType, 
                    OrderDate = DateTime.Now,
                    OrderTotal = cart.Sum(x => x.MenuItem.Price * x.Quantity)
                };

                _context.OrderHeaders.Add(orderHeader);
                await _context.SaveChangesAsync();

                // Create Details
                foreach (var item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderHeaderId = orderHeader.Id,
                        MenuItemId = item.MenuItem.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Quantity
                    };
                    _context.OrderDetails.Add(orderDetail);
                }

                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("MyCart");

                return RedirectToAction("OrderConfirmation", new { id = orderHeader.Id });
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}