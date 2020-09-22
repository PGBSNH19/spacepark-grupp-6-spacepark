using System.Threading.Tasks;
using spaceparkapi.Models;

namespace spaceparkapi.Services.Interfaces
{
    public interface ITravellerRepository : IRepository
    {
        Task<bool> IsFamous(string v);
        Task<Traveller> GetTravellerByName(string name);
        Task<Traveller> RegisterTraveller(string name);
    }
}
