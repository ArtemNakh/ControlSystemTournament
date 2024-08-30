using ControlSystemTournament.Core.Models;
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
