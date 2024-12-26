using ClassifiedAds.Helpers;
using ClassifiedAds.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClassifiedAds.Controllers
{
    public class AccountController(IHttpContextAccessor httpContextAccessor) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // if user is valid
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = true, IsEssential = true };
            if (model.RememberMe)
            {
                cookieOptions.Expires = DateTime.UtcNow.AddDays(30);
            }

            AuthCookieUser user = new AuthCookieUser
            {
                Roles = new List<string> { "Admin", "Moderator" },
                UserName = model.UserName,
            };

            var serializedUser = JsonConvert.SerializeObject(user);
            var encryptedUser = EncryptionHelper.EncryptString(serializedUser);

            httpContextAccessor.HttpContext.Response.Cookies.Append(Globals.AuthCookieName, encryptedUser, cookieOptions);

            return RedirectToAction("Index", "Home");
        }
    }
}
