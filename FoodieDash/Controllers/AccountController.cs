using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using FoodieDash.Data;
using FoodieDash.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace FoodieDash.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login Page
        public IActionResult Login()
        {
            // If already logged in, go to Home
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Handle Login Logic
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // 1. Check if user exists in DB
            var user = _context.AppUsers.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // 2. Create User Identity (The "ID Card" for the session)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 3. Sign In (Creates the encrypted cookie)
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            // 4. Login Failed
            ViewBag.Error = "Invalid Username or Password";
            return View();
        }

        // GET: Register Page
        public IActionResult Register()
        {
            return View();
        }

        // POST: Handle Register Logic
        [HttpPost]
        public IActionResult Register(string username, string email, string password, string confirmPassword)
        {
            // 1. Basic Validation
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match!";
                return View();
            }

            // 2. Check if username/email already exists
            if (_context.AppUsers.Any(u => u.Username == username || u.Email == email))
            {
                ViewBag.Error = "Username or Email already taken.";
                return View();
            }

            // 3. Save User
            var newUser = new AppUser
            {
                Username = username,
                Email = email,
                Password = password, // Storing as plain text for simplicity (In real apps, hash this!)
                Role = "User"
            };

            _context.AppUsers.Add(newUser);
            _context.SaveChanges();

            // 4. Redirect to Login
            return RedirectToAction("Login");
        }

        // Logout Logic
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}