using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface IPlayerRoleService
    {
        Task<PlayerRole> GetPLayerRoleByIdAsync(int id);
        Task<IEnumerable<PlayerRole>> GetAllPLayerRolesAsync();
        Task<PlayerRole> GetPLayerRoleByNameAsync(string nameRole);
        Task<PlayerRole> CreatePlayerRoleAsync(string nameRole);
        Task DeletePlayerRoleAsync(int id);
    }
}
