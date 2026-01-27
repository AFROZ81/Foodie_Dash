using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodieDash.Data;
using FoodieDash.Models;
using Microsoft.AspNetCore.Hosting;

namespace FoodieDash.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // Add this variable

        // Update Constructor
        public MenuItemController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment; // Initialize it
        }
        // GET: MenuItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItems.Include(m => m.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItem/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: MenuItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, MenuItem menuItem, IFormFile? file)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // 1. Check if a file was uploaded
                if (file != null && file.Length > 0)
                {
                    // 2. Get the path to wwwroot/images
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // Create a random name so files don't overwrite each other
                    string productPath = Path.Combine(wwwRootPath, @"images");

                    // 3. Save the file to disk
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // 4. Save the relative URL to the database
                    menuItem.ImageUrl = @"/images/" + fileName;
                }
                else
                {
                    // Optional: Set a default image if they didn't upload one
                    menuItem.ImageUrl = "https://placehold.co/600x400";
                }

                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuItem);
        }

        // GET: MenuItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", menuItem.CategoryId);
            return View(menuItem);
        }

        // In MenuItemController.cs

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Make sure IFormFile? file is in the parameters!
        public async Task<IActionResult> Edit(int id, MenuItem menuItem, IFormFile? file)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // === FILE UPLOAD LOGIC START ===
                    if (file != null && file.Length > 0) // Check if a new file was chosen
                    {
                        // 1. Get root path
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        // 2. Create unique filename
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        // 3. Define the full path to save the file
                        string productPath = Path.Combine(wwwRootPath, @"images");

                        // IMPORTANT: Ensure the directory exists
                        if (!Directory.Exists(productPath))
                        {
                            Directory.CreateDirectory(productPath);
                        }

                        // 4. Save the file to the disk
                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // 5. Update the model's ImageUrl property
                        menuItem.ImageUrl = @"/images/" + fileName;
                    }
                    // === FILE UPLOAD LOGIC END ===

                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // ... (error handling code)
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menuItem);
        }

        // GET: MenuItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
