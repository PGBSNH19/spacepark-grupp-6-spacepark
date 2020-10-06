using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceparkWebApp.Models;
using System;
using System.Collections.Generic;
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
                TempData.Clear();

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    TempData["msg"] = "Please enter traveller name.";
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TempData["msg"] = "The traveller has not been in a Star Wars movie and is therefore not authorized.";
                    return RedirectToAction("Index");
                }

                Traveller Traveller = JsonConvert.DeserializeObject<Traveller>(response.Content.ReadAsStringAsync().Result);
                Traveller.Parkingspots = GetParking().Result.ToList();
                return View("Details", Traveller);
            }
            catch (Exception)
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

        public async Task<IActionResult> Park(int spaceshipId, string travellerName)
        {
            HttpClient _client = new HttpClient();
            var spaceship = GetSpaceship(spaceshipId);
            int spaceshipLength = Convert.ToInt32(spaceship.Result.Length);
            Traveller travellerResult = GetTraveller(travellerName).Result;
            TempData.Clear();

            if (spaceshipLength < 500)
            {
                try
                {
                    //Get the first free parking...
                    var parkingUrl = _configuration["ApiHostUrl"] + "/api/v1.0/spaceport/getparkingspot/?spaceshipLength=" + spaceshipLength;
                    HttpResponseMessage parkResponse = await _client.GetAsync(parkingUrl);
                    var parking = JsonConvert.DeserializeObject<Parkingspot>(parkResponse.Content.ReadAsStringAsync().Result);

                    var json = JsonConvert.SerializeObject(parking);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    string url = _configuration["ApiHostUrl"] + "/api/v1.0/Parkingspot/park?parkingId=" + parking.Id + "&spaceshipId=" + spaceshipId;
                    HttpResponseMessage response = await _client.PutAsync(url, content);

                    travellerResult = GetTraveller(travellerName).Result;
                    return View("Details", travellerResult);
                }
                catch (Exception)
                {
                    TempData["error"] = "There is no free parkings...!";
                    return View("Details", travellerResult);
                } 
            }
            else
            {
                TempData["error"] = "You can not park here therefore your ship is very long...!";
                return View("Details", travellerResult);
            }
        }

        public async Task<IActionResult> Checkout(int parkingId, string travellerName)
        {
            HttpClient _client = new HttpClient();
            Traveller travellerResult = GetTraveller(travellerName).Result;

            try
            {
                var parkings = GetParking().Result.ToList();
                var parking = parkings.Where(x => x.ParkedSpaceship.Id == parkingId);

                var json = JsonConvert.SerializeObject(parking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string url = _configuration["ApiHostUrl"] + "/api/v1.0/Parkingspot/checkout?parkingId=" + parkingId;
                HttpResponseMessage response = await _client.PutAsync(url, content);

                travellerResult = GetTraveller(travellerName).Result;
                return View("Details", travellerResult);
            }
            catch (Exception)
            {
                TempData["msg"] = "There is something wrong...!";
                return View("Details", travellerResult);
            }
        }

        public async Task<List<Parkingspot>> GetParking()
        {
            HttpClient _client = new HttpClient();

            var url = _configuration["ApiHostUrl"] + "/api/v1.0/parkingspot/";
            var response = await _client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var parkingspotResults = JsonConvert.DeserializeObject<List<Parkingspot>>(response.Content.ReadAsStringAsync().Result);

            foreach (var park in parkingspotResults)
            {
                if (park.ParkedSpaceship == null)
                {
                    park.ParkedSpaceship = new Spaceship { Id = 0 };
                }
            }

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
            travellerResult.Parkingspots = GetParking().Result.ToList();
            return travellerResult;
        }
    }
}