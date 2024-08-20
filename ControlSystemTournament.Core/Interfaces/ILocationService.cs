using ControlSystemTournament.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationByIdAsync(int id);
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location> CreateLocationAsync(Location location);
        Task UpdateLocationAsync(Location location);
        Task DeleteLocationAsync(int id);
    }
}
