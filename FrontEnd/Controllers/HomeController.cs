using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceparkWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpaceparkWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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
                return View("Error");
            }
        }

        public  IActionResult Create(string Name)
        {
<<<<<<< Updated upstream
            return View("Create");
        }
=======
            HttpClient _client = new HttpClient();
            Traveller travellerResult = GetTraveller(name).Result;

            try
            {
                //Get the first free parking...
                var spaceship = GetSpaceship(id);
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
>>>>>>> Stashed changes

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
