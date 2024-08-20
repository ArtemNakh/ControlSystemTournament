using ControlSystemTournament.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface IPlayerRoleService
    {
        Task<PlayerRole> GetPLayerRoleByIdAsync(int id);
        Task<IEnumerable<PlayerRole>> GetAllPLayerRolesAsync();
        Task<IEnumerable<PlayerRole>> GetPLayerRoleByNameAsync(string nameRole);
        Task<PlayerRole> CreatePlayerRoleAsync(string nameRole);
        Task DeletePlayerRoleAsync(int id);
    }
}
