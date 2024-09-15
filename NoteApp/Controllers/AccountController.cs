using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.Services;
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
                // Set authentication cookies or tokens
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult ConfirmEmail() => View();

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail(EmailConfirmationViewModel model)
    {
        if (ModelState.IsValid)
        {
            bool isConfirmed = await _userService.ConfirmEmailAsync(model);
            if (isConfirmed)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Invalid confirmation attempt.");
        }
        return View(model);
    }
}