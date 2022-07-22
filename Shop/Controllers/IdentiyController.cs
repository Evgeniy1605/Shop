using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Security.Claims;

namespace Shop.Controllers
{
    public class IdentiyController : Controller
    {
        private readonly OrderDbConenction _content;
        public IdentiyController(OrderDbConenction content)
        {
            _content = content;
        }
        [Authorize]
        public IActionResult SecretPage()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult logAsAdmin(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> logAsAdmin(string username, string password, string returnUrl)
        {
            var AllAdmins = _content.Admins.ToList();
            var AllUsers = _content.Users.ToList();
            foreach (var admin in AllAdmins)
            {
                if (username == admin.Name && password == admin.PassWord)
                {
                    var calaims = new List<Claim>();
                    calaims.Add(new Claim("username", admin.Name));
                    calaims.Add(new Claim(ClaimTypes.NameIdentifier, admin.Name));
                    calaims.Add(new Claim(ClaimTypes.Name, admin.Name));

                    calaims.Add(new Claim(ClaimTypes.Role, "Admin"));

                    var claimsIdentity = new ClaimsIdentity(calaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return View("Secseeded");

                }
                foreach (var user in AllUsers)
                {
                    if (username == user.Name && password == user.PassWord)
                    {
                        var calaims = new List<Claim>();
                        calaims.Add(new Claim("Id", user.Id.ToString()));
                        calaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        calaims.Add(new Claim(ClaimTypes.Name, user.Name));
                        calaims.Add(new Claim("Email", user.Email));
                        calaims.Add(new Claim("Phone", user.PhoneNumber));
                        calaims.Add(new Claim("SumOfAllPurchases", user.SumOfAllPurchases.ToString()));
                        calaims.Add(new Claim(ClaimTypes.Role, "User"));
                        calaims.Add(new Claim("Surename", user.Surname));
                        calaims.Add(new Claim("Password", user.PassWord));

                        var claimsIdentity = new ClaimsIdentity(calaims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        return Redirect("/");
                    }
                }
            }
            return View("failed");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        [Authorize]
        public IActionResult UserPage()
        {
            return View();
        }

        [HttpGet("denied")]
        [Authorize]
        public IActionResult denied()
        {
            return View();
        }

    }
}
