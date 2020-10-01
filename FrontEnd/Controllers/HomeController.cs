using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceparkWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        public Traveller Traveller;
        public Parkingspot parking;
        public List<Spaceship> Spaceships;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string name)
        {
            HttpResponseMessage response = null;

            try
            {
                HttpClient _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("name", "" + name);
                var url = _configuration["ApiHostUrl"] + "/api/v1.0/traveller/auth";
                response = await _client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TempData["msg"] = "The traveller has not been in a Star Wars movie and is therefore not authorized.";
                    return RedirectToAction("Index");
                }

                Traveller = JsonConvert.DeserializeObject<Traveller>(response.Content.ReadAsStringAsync().Result);
                return View("Edit", this);
            }
            catch (Exception ex)
            {
                if (response == null)
                {
                    TempData["msg"] = "The API did not return any response.";
                }
                else
                {
                    TempData["msg"] = $"An unexpected error has ocurred: {response.StatusCode.ToString()}";
                }
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
                var spaceship = GetSpaceship(id);
                var parkingUrl = _configuration["ApiHostUrl"] + "/api/v1.0/spaceport/getparkingspot/?spaceshipLength=" + (Convert.ToInt32(spaceship.Result.Length)).ToString();
                string parkResponse = await _client.GetStringAsync(parkingUrl);
                var parking = JsonConvert.DeserializeObject<Parkingspot>(parkResponse);

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

                return View("Edit", travellerResult);
            }
            catch (Exception)
            {
                TempData["msg"] = "There is something wrong...!";
                return RedirectToAction("Details", travellerResult);
            }
        }

        public async Task<Parkingspot> GetParking(int spaceshipId)
        {
            HttpClient _client = new HttpClient();

            var url = _configuration["ApiHostUrl"] + "/api/v1.0/parkingspot/" + spaceshipId;
            var response = await _client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var parkingspotResults = JsonConvert.DeserializeObject<Parkingspot>(response.Content.ReadAsStringAsync().Result);

            return parkingspotResults;
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

        public IActionResult Unpark(string Name)
        {
            var traveller = "";
            return View("Details", traveller);
        }
    }
}