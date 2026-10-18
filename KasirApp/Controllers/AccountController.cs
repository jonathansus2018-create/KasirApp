using KasirApp.Services;
using KasirApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KasirApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService) => _authService = authService;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _authService.ValidateAsync(vm.Username, vm.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Username atau password salah.");
                return View(vm);
            }

            await SignInAsync(user);

            // Redirect berdasarkan role
            return user.Role == "Admin"
                ? RedirectToAction("Index", "Home")
                : RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var (success, error, _) = await _authService.RegisterAsync(
                vm.Username, vm.FullName, vm.Password, vm.Role);

            if (!success)
            {
                ModelState.AddModelError("", error!);
                return View(vm);
            }

            TempData["Success"] = "Registrasi berhasil. Silakan login.";
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied() => View();

        // 🎯 Ini kunci: bikin ClaimsIdentity dengan ROLE
        private async Task SignInAsync(KasirApp.Models.MsUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.IdUser),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.GivenName, user.FullName),
                new(ClaimTypes.Role, user.Role)   // ⚠️ WAJIB agar [Authorize(Roles=...)] jalan
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });
        }
    }
}