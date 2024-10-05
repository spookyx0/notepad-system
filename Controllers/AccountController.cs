using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyNotesApp.Models;
using MyNotesApp.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyNotesApp.Controllers;

    public class AccountController : Controller
    {
        private readonly MyNotesAppContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(MyNotesAppContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger; // Assign the logger
        }



        // GET: AccountController
        public IActionResult Login()
        {
            return View();
        }

      [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Login(LoginModel model)
        {
            // Log the login attempt with the email provided
            _logger.LogInformation("Login attempt for email: {Email}", model.Email);

            if (ModelState.IsValid)
            {
                // Log if the model state is valid
                _logger.LogInformation("Model state is valid for email: {Email}", model.Email);

                // Query the UserModel for the user based on the email and password
                var user = await _context.UserModel.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Log successful login
                    _logger.LogInformation("User {UserId} logged in successfully.", user.UserId);

                    // Create claims for the user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
                    };

                    // Create claims identity
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Log invalid login attempt
                    _logger.LogWarning("Invalid login attempt for email: {Email}. User not found.", model.Email);
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            else
            {
                // Log invalid model state
                _logger.LogWarning("Model state is invalid for email: {Email}.", model.Email);
            }

            // Return the view with the model
            return View(model);
        }


            }

