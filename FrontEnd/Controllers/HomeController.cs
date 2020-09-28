using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceparkWebApp.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpaceparkWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Name { get; set; }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Authenticate(string Name)
        {
            HttpClient _client = new HttpClient();

            try
            {

                _client.DefaultRequestHeaders.Add("name", "" + Name);
                var url = "http://localhost:11725/api/v1.0/traveller/auth";
                string response = await _client.GetStringAsync(url);

                Traveller Traveller = JsonConvert.DeserializeObject<Traveller>(response);
                return View("Details", Traveller);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public  IActionResult Create(string Name)
        {
            return View("Create");
        }

        public IActionResult Edit(string Name)
        {
            return View("Edit");
        }

        public IActionResult Delete(string Name)
        {
            return View("Details");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
