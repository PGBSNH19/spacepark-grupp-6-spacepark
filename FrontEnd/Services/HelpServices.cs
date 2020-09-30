using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SpaceparkWebApp.Models;
using Microsoft.Extensions.Configuration;

namespace SpaceparkWebApp.Services
{
    public  class HelpServices
    {
        private readonly IConfiguration _configuration;
        public HelpServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
