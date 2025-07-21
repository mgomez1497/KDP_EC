using KDP_EC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDP_EC.Core.ModelView;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace KDP_EC.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            ViewBag.OcultarHeader = true;
            ViewBag.FondoEspecial = true;
            ViewBag.OcultarMenu = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Core.Models.UserLogin model)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var apiUrl = "https://localhost:7149/api/Account/login";
            Console.WriteLine($"🔹 Llamando a la API en: {apiUrl}");

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
                return View("Index");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponse>(responseString);

            
            HttpContext.Session.SetString("AuthToken", result.Token);
            HttpContext.Session.SetString("UserId", result.UserId.ToString());

            return RedirectToAction("GetUserInfo");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }

            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            
            var apiUrl = $"https://localhost:7149/api/Account/userinfo?UserId={userId}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<UserInfoViewModel>>(responseString);

            HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(result));

            //recuperar la información del usuario

            //var userInfoJson = HttpContext.Session.GetString("UserInfo");

            //if (!string.IsNullOrEmpty(userInfoJson))
            //{
            //    var userInfo = JsonConvert.DeserializeObject<List<UserInfoViewModel>>(userInfoJson);
            //}


            return RedirectToAction("Index", "Home");
        }




        [HttpPost]
        public async Task<IActionResult> CreateUser(Users model)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var apiUrl = "https://localhost:7149/api/Account/createUser";

            var userToSend = new Users
            {
                Id = Guid.NewGuid(), 
                UserName = model.UserName, 
                Password = model.Password
            };

            var json = JsonConvert.SerializeObject(userToSend);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al crear el usuario.");
                return View("Index");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<Users>(responseContent);

            var userId = userToSend.Id;
            return RedirectToAction("CreatedUrc", "KDP_EC", new { userId = userId });
        }


        private async Task CargarPaisesAsync()
        {
            var apiUrl = "https://localhost:7149/api/Country/getCountries";
            Console.WriteLine($"🔹 Llamando a la API en: {apiUrl}");

            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Countries = new List<SelectListItem>();
                return;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<List<Countries>>(responseContent);

            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }
        public IActionResult Password()
        {
            var identification = TempData["Identification"] as string;
            ViewBag.Identification = identification;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Account");
        }
    }
}
