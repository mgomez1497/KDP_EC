using KDP_EC.Core.ModelView;
using KDP_EC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KDP_EC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var userInfoJson = HttpContext.Session.GetString("UserInfo");

            if (!string.IsNullOrEmpty(userInfoJson))
            {
                var userInfo = JsonConvert.DeserializeObject<List<UserInfoViewModel>>(userInfoJson);

                if (userInfo != null && userInfo.Count > 0)
                {
                    var user = userInfo[0];
                    ViewBag.NombreCompleto = $"{user.NombreCompleto}";
                    ViewBag.Identification = $"{user.Identification}";
                }
            }
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
