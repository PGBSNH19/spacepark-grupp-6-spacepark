using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceparkWebApp.Models;
using SpaceparkWebApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpaceparkWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HelpServices _helpServices;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, HelpServices helpServices)
        {
            _logger = logger;
            _configuration = configuration;
            _helpServices = helpServices;
        }

        public string Name { get; set; }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Authenticate(string name)
        {
            try
            {
                HttpClient _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("name", "" + name);
                var url = _configuration["ApiHostUrl"] + "/api/v1.0/traveller/auth";
                string response = await _client.GetStringAsync(url);

                Traveller Traveller = JsonConvert.DeserializeObject<Traveller>(response);
                return View("Details", Traveller);
            }
            catch (Exception)
            {
                TempData["msg"] = "API program is not running or the person is not from SpacPark database...!";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Park(int id, string name)
        {
            HttpClient _client = new HttpClient();
            Traveller travellerResult = GetTraveller(name).Result;

            try
            {
                //Get the first free parking...
                var spaceship = _helpServices.GetSpaceship(id);
                var parkingUrl = _configuration["ApiHostUrl"] + "/api/v1.0/spaceport/getparkingspot/?spaceshipLength=" + (Convert.ToInt32(spaceship.Result.Length)).ToString();
                string parkResponse = await _client.GetStringAsync(parkingUrl);
                Parkingspot parking = JsonConvert.DeserializeObject<Parkingspot>(parkResponse);

                //Park the spaceship in this parking...
                Parkingspot parkingspot = new Parkingspot()
                {
                    SpaceportId = 500,
                    ParkedSpaceshipId = id
                };

                var json = JsonConvert.SerializeObject(parkingspot);

                var content = new StringContent(json, Encoding.UTF8, "application/json");


                string url = _configuration["ApiHostUrl"] + "/api/v1.0/parkingspot/parkSpaceship/";
                var response = await _client.PutAsync(url, content);


                return View("Details", travellerResult);
            }
            catch (Exception)
            {
                TempData["msg"] = "There is something wrong...!";
                return RedirectToAction("Details", travellerResult);
            }
        }

        public async Task<IActionResult> Unpark(string Name)
        {
            var traveller = "";
            return View("Details", traveller);
        }




        //Service Methods
        public async Task<Parkingspot> GetParking(int spaceshipId)
        {
            HttpClient _client = new HttpClient();

            var url = _configuration["ApiHostUrl"] + "/api/v1.0/parkingspot/";
            string response = await _client.GetStringAsync(url);

            List<Parkingspot> parkingspotResults = JsonConvert.DeserializeObject<List<Parkingspot>>(response);
            Parkingspot resultparking = parkingspotResults.Where(x => x.ParkedSpaceshipId == spaceshipId).FirstOrDefault();
            return resultparking;
        }

        public async Task<IEnumerable<Spaceship>> GetSpaceshipsParking(int TravellerId)
        {
            HttpClient _client = new HttpClient();

            var url = _configuration["ApiHostUrl"] + "/api/v1.0/Spaceport/traveller/" + TravellerId;
            string response = await _client.GetStringAsync(url);

            SpaceshipResults spaceshipResults = JsonConvert.DeserializeObject<SpaceshipResults>(response);
            return spaceshipResults.results.Where(x => x.Traveller.Id == TravellerId);
        }

        public async Task<Spaceship> GetSpaceship(int spaceshipId)
        {
            HttpClient _client = new HttpClient();

            var url = _configuration["ApiHostUrl"] + "/api/v1.0/Spaceship";
            string response = await _client.GetStringAsync(url);

            List<Spaceship> spaceshipResults = JsonConvert.DeserializeObject<List<Spaceship>>(response);

            var spaceshipResult = spaceshipResults.Where(x => x.Id == spaceshipId).FirstOrDefault();
            return spaceshipResult;
        }

        public async Task<Traveller> GetTraveller(string travellerName)
        {
            HttpClient _client = new HttpClient();

            var url = _configuration["ApiHostUrl"] + "/api/v1.0/Traveller";
            string response = await _client.GetStringAsync(url);

            List<Traveller> ravellerResults = JsonConvert.DeserializeObject<List<Traveller>>(response);

            var travellerResult = ravellerResults.Where(x => x.Name == travellerName).FirstOrDefault();
            return travellerResult;
        }
    }
}
