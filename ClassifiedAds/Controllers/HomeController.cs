using ClassifiedAds.Helpers;
using ClassifiedAds.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassifiedAds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            var v1 = _contextAccessor.HttpContext.Request.Cookies["AuthV1"];
            var user = new
            {
                Name = "Abdul Majeed",
                Field = "Software Development",
                Id = 10,
                Random = Guid.NewGuid().ToString(),
                Roles = new[] { "Admin", "Moderator" }
            };

            var serializedUser = JsonConvert.SerializeObject(user);
            var encryptedUser = EncryptionHelper.EncryptString(serializedUser);

            _contextAccessor.HttpContext.Response.Cookies.Append("AuthV2", encryptedUser, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true
            });

            return View();
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
