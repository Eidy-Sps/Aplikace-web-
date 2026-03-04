using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEB.Data;
using WEB.Models;

namespace WEB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            FakeDatabase.Users.Add(new User { Email = email, Password = password });
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = FakeDatabase.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Špatné jméno nebo heslo";
                return View();
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Profile");
        }

        [Authorize]
        public IActionResult Profile() => View();

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}