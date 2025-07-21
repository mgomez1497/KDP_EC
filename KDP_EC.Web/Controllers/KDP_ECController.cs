using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using KDP_EC.Core.ModelView;
using KDP_EC.Infraestructure.Implementations.EC_KDP;
using KDP_EC.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using System;
using System.Security.Permissions;
using System.Text;

namespace KDP_EC.Web.Controllers
{

    public class KDP_ECController : Controller
    {
        private readonly HttpClient _httpClient;

        IChains chainsRepository;
        ICities citiesRepository;
        ICountries countriesRepository;
        IStates statesRepository;
        IFarms farmsRepository;
        IPerson personRepository;
        ILots lotsRepository;

        public KDP_ECController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            this.chainsRepository = new ChainsRepository();

        }


        #region Chains
        public ActionResult Chains()
        {
            return View(chainsRepository.GetChains());
        }
        #endregion

        #region Person
        [HttpPost]

        public async Task<IActionResult> CreatePerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                await GetCountries();
                return View("~/Views/Person/Register.cshtml", person);
            }

            var apiUrl = "https://localhost:7149/api/Person/postPersons";

            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ Error en respuesta de la API: " + errorContent);


                ModelState.AddModelError(string.Empty, "Error al crear la persona.");
                return View("~/Views/Person/Register.cshtml", person);

            }

            TempData["Identification"] = person.Identification;

            return RedirectToAction("Password", "Account");
        }

        public async Task<List<Person>> GetPerson()
        {
            var apiUrl = "https://localhost:7149/api/Person/getPersons";
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<Person>();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var persons = JsonConvert.DeserializeObject<List<Person>>(responseContent);
            return persons;
        }

        public async Task<IActionResult> Register()
        {
            await GetCountries();
            return View("~/Views/Person/Register.cshtml");
        }

        public async Task<IActionResult> IndexPerson(string search)
        {
            var persons = await GetPerson();

            if (!string.IsNullOrEmpty(search))
            {
                persons = persons
                    .Where(p => p.Identification != null && p.Identification.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search;
            return View("~/Views/Person/IndexPerson.cshtml", persons);
        }
        public async Task<IActionResult> Home()
        {
            await GetPerson();
            return View("~/Views/Person/Home.cshtml");
        }

        #endregion

        #region Countries
        [HttpGet]
        private async Task GetCountries()
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
        #endregion

        #region Companies
        [HttpGet]

        private async Task GetCompanies()
        {
            var apiUrl = "https://localhost:7149/api/Company/getCompanies";
            Console.WriteLine($"🔹 Llamando a la API en: {apiUrl}");
            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Companies = new List<SelectListItem>();
                return;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var companies = JsonConvert.DeserializeObject<List<Company>>(responseContent);
            ViewBag.Companies = companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }

        #endregion

        #region Rols
        [HttpGet]

        private async Task GetRols()
        {
            var apiUrl = "https://localhost:7149/api/Rols/getRols";
            Console.WriteLine($"🔹 Llamando a la API en: {apiUrl}");
            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Rols = new List<SelectListItem>();
                return;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var rols = JsonConvert.DeserializeObject<List<Rols>>(responseContent);
            ViewBag.Rols = rols.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
        }

        #endregion

        #region URC
        public async Task<IActionResult> CreateURC(URC urc)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/URC/Create.cshtml", urc);
            }

            var apiUrl = "https://localhost:7149/api/URC/createURC";

            var json = JsonConvert.SerializeObject(urc);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ Error en respuesta de la API: " + errorContent);

                ModelState.AddModelError(string.Empty, "Error al crear la persona.");
                return View("~/Views/Person/Register.cshtml", urc);
            }

            return RedirectToAction("Index", "Account");

        }

        public async Task<IActionResult> CreatedUrc(Guid userId)
        {
            await GetCompanies();
            await GetRols();

            var urc = new URC
            {
                Id_User = userId
            };

            return View("~/Views/URC/Create.cshtml", urc);
        }
        #endregion

        #region Farms



        [HttpGet]
        public async Task<IActionResult> GetFarmsByPersonId(string identification)
        {
            var apiUrl = $"https://localhost:7149/api/Farms/getFarmsByPersonId?identification={identification}";
            Console.WriteLine($"🔹 Llamando a la API en: {apiUrl}");

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                // Verificar si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var farms = JsonConvert.DeserializeObject<List<FarmInfoViewModel>>(json);
                    var haTotales = farms.Sum(f => f.Ha_Totales);

                    ViewBag.Identification = identification;
                    ViewBag.TotalFincas = farms.Count;
                    ViewBag.HaTotales = haTotales;
                    return View("IndexFarms", farms);
                }
                else
                {
                    // Detalles del error si la respuesta no es exitosa
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error al obtener las fincas. Código de estado: {response.StatusCode}, Mensaje: {errorMessage}");
                    return View("IndexFarms", new List<FarmInfoViewModel>());
                }
            }
            catch (Exception ex)
            {
                // Captura de excepciones (por ejemplo, problemas de red, configuración incorrecta, etc.)
                Console.WriteLine($"⚠️ Excepción al llamar a la API: {ex.Message}");
                return View("IndexFarms", new List<FarmInfoViewModel>());
            }
        }



        public async Task<IActionResult> IndexFarms(string identification)
        {
            await GetFarmsByPersonId(identification);
            return View("~/Views/Farms/IndexFarms.cshtml");
        }

        #endregion

        #region Lots

        [HttpGet]
        public async Task<List<Lots>> GetLotsByFarmId(Guid fincaId, Guid? tipoLote = null,
                                                      Guid? variedadLote = null, Guid? tipoRenovacion = null)
        {
            var queryParams = $"FarmId={fincaId}";

            if (tipoLote.HasValue)
                queryParams += $"&TipoLote={tipoLote.Value}";
            if (variedadLote.HasValue)
                queryParams += $"&VariedadLote={variedadLote.Value}";
            if (tipoRenovacion.HasValue)
                queryParams += $"&TipoRenovacion={tipoRenovacion.Value}";

            var apiUrl = $"https://localhost:7149/api/Lots/getLotsByFarmId?{queryParams}";
            Console.WriteLine($"🔹 Llamando a la API en: {apiUrl}");

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"🔹 Respuesta de la API: {json}");
                    var lots = JsonConvert.DeserializeObject<List<Lots>>(json);

                    var hoy = DateTime.Now;
                    var enLevante = lots.Count(l => l.WorkDate != null && hoy.Subtract(l.WorkDate.Value).TotalDays < 730);
                    var productivo = lots.Count(l => l.WorkDate != null && hoy.Subtract(l.WorkDate.Value).TotalDays >= 730);
                    ViewBag.EnLevante = enLevante;
                    ViewBag.Productivo = productivo;
                    return lots;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error al obtener los lotes. Código de estado: {response.StatusCode}, Mensaje: {errorMessage}");
                    return new List<Lots>();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Excepción al llamar a la API: {ex.Message}");
                return new List<Lots>();
            }
        }

        public async Task<IActionResult> IndexLots(Guid fincaId)
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

            var farms = await GetFarmsByPerson(ViewBag.Identification);
            var lotsTypes = await GetLotsTypes();
            ViewBag.lotsTypes = lotsTypes;
            var renewalTypes = await GetRenewalTypes();
            ViewBag.renewalTypes = renewalTypes;
            var lotsVarietys = await GetLotsVarietys();
            ViewBag.lotsVarietys = lotsVarietys;
            ViewBag.Farms = farms;



            return View("~/Views/Lots/IndexLots.cshtml");
        }


        public async Task<IActionResult> LoteIndicadores(Guid fincaId)
        {
            var lots = await GetLotsByFarmId(fincaId);

            var hoy = DateTime.Now;
            var enLevante = lots.Count(l => l.WorkDate != null && hoy.Subtract(l.WorkDate.Value).TotalDays < 730);
            var productivo = lots.Count(l => l.WorkDate != null && hoy.Subtract(l.WorkDate.Value).TotalDays >= 730);

            var resultado = new
            {
                EnLevante = enLevante,
                Productivo = productivo
            };

            return View(resultado);
        }

        private async Task<List<FarmInfoViewModel>> GetFarmsByPerson(string identification)
        {
            var apiUrl = $"https://localhost:7149/api/Farms/getFarmsByPersonId?identification={identification}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var farms = JsonConvert.DeserializeObject<List<FarmInfoViewModel>>(json);
                return farms;
            }

            return new List<FarmInfoViewModel>();
        }

        private async Task<List<Lots_Type>> GetLotsTypes()
        {
            var apiUrl = "https://localhost:7149/api/Lots_Type/getLotsType";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var lotsType = JsonConvert.DeserializeObject<List<Lots_Type>>(json);
                return lotsType;
            }

            return new List<Lots_Type>();
        }

        private async Task<List<Lots_Varietys>> GetLotsVarietys()
        {
            var apiUrl = "https://localhost:7149/api/Lots_Varietys/getLotsVarietys";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var lotsVarietys = JsonConvert.DeserializeObject<List<Lots_Varietys>>(json);
                return lotsVarietys;
            }

            return new List<Lots_Varietys>();
        }

        private async Task<List<Renewal_Types>> GetRenewalTypes()
        {
            var apiUrl = "https://localhost:7149/api/Renewal_Types/getRenewalTypes";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var renewalTypes = JsonConvert.DeserializeObject<List<Renewal_Types>>(json);
                return renewalTypes;
            }

            return new List<Renewal_Types>();
        }
        #endregion

        #region ExpTec

        [HttpGet]
        public async Task<List<ExportTecnViewModel>> GetExportTecn(Guid ExpId)
        {
            var apiUrl = $"https://localhost:7149/api/ExporTec/getTecnicianbyExport?ExpId={ExpId}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var exportec = JsonConvert.DeserializeObject<List<ExportTecnViewModel>>(json);
                return exportec;
            }

            return new List<ExportTecnViewModel>();
        }

        public async Task<IActionResult> IndexExportTecn(Guid ExpId)
        {
            var exportec = await GetExportTecn(ExpId);
            return View("~/Views/Tecnician/IndexTecnician.cshtml", exportec);
        }

        #endregion
    }
}

