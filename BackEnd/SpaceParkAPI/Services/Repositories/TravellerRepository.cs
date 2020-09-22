using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using spaceparkapi.DBContext;
using spaceparkapi.Models;
using spaceparkapi.Services.Interfaces;
using spaceparkapi.Swapi;

namespace spaceparkapi.Services.Repositories
{
    public class TravellerRepository : Repository, ITravellerRepository
    {
        private readonly SpaceContext _context;
        private readonly ILogger<TravellerRepository> _logger;

        public TravellerRepository(SpaceContext context, ILogger<TravellerRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Traveller> GetTravellerByName(string name)
        {
            _logger.LogInformation($"Fetching traveller with name {name} from the database.");
            return await _context.Set<Traveller>().Include("Spaceships").FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<Traveller> RegisterTraveller(string name)
        {
            _logger.LogInformation($"Adding traveller with name {name} to the database.");
            Traveller traveller = new Traveller()
            {
                Name = name,
                Spaceships = GetSwapiSpaceships(name).Result
            };
            await _context.Set<Traveller>().AddAsync(traveller);
            await Save();
            return await GetTravellerByName(name);
        }

        public async Task<bool> IsFamous(string travellerName)
        {
            var httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://swapi.dev/api/people/?search=" + travellerName);
            PersonResults personResults = JsonConvert.DeserializeObject<PersonResults>(json);

            return personResults.results.Where(x => x.name.ToLower() == travellerName.ToLower()).Any();
        }

        public async Task<List<Spaceship>> GetSwapiSpaceships(string travellerName)
        {
            List<Spaceship> spaceships = new List<Spaceship>();
            var httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync("https://swapi.dev/api/people/?search=" + travellerName);
            PersonResults personResults = JsonConvert.DeserializeObject<PersonResults>(json);
            List<string> spaceShipUrls = personResults.results.Where(x => x.name.ToLower() == travellerName.ToLower()).FirstOrDefault().starships;

            foreach (string spaceShipUrl in spaceShipUrls)
            {
                string spaceshipjson = await httpClient.GetStringAsync(spaceShipUrl);
                Starship starship = JsonConvert.DeserializeObject<Starship>(spaceshipjson);

                spaceships.Add(new Spaceship()
                {
                    Name = starship.name,
                    Length = starship.length
                });
            }

            return spaceships;
        }
    }
}
