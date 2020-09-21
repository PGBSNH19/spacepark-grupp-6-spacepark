using System.Threading.Tasks;
using spaceparkapi.Models;

namespace spaceparkapi.Services.Interfaces
{
    public interface IParkingspotRepository : IRepository
    {
        Task<Parkingspot> GetParkingSpotInfoById(int id);
    }
}