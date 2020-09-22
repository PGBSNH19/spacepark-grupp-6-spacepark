using System.Collections.Generic;
using System.Threading.Tasks;
using spaceparkapi.Models;

namespace spaceparkapi.Services.Interfaces
{
    public interface ISpaceportRepository : IRepository
    {
        Task<IList<Parkingspot>> GetTravellerParkingspots(int travellerId);
    }
}