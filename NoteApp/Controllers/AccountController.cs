using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoteApp.Controllers;

public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _userService.RegisterUserAsync(model);
            return RedirectToAction("Login");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.AuthenticateUserAsync(model);
            if (user != null)
            {
                // Authenticate the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username)
                // Add additional claims as needed
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Optional: Add any additional properties here
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                // Set authentication cookies or tokens
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmailAsync(string username, string token)
    {
        // Return a view where users can confirm their email with the provided email and token
        var model = new EmailConfirmationViewModel { Username = username, Token = token };
        if (ModelState.IsValid)
        {
            bool isConfirmed = await _userService.ConfirmEmailAsync(model);
            if (isConfirmed)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Invalid confirmation attempt.");
        }
        return RedirectToAction("Register");
    }
}