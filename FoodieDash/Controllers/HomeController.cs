using FoodieDash.Data;
using FoodieDash.Models;             // Required to see MenuItem class
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for database access
using System.Diagnostics;
// (Make sure your namespace matches your project name)

namespace FoodieDash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // 1. Add Database Context

        // 2. Inject Database Context in Constructor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // 3. Update Index to fetch items
        public async Task<IActionResult> Index()
        {
            // You MUST have .Include(m => m.Category) or the category names will be null!
            var items = await _context.MenuItems
                                      .Include(m => m.Category)
                                      .ToListAsync();

            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}