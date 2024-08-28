using ControlSystemTournament.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetPlayersTournamentAsync(int tournamentId);
        Task<IEnumerable<Player>> GetPlayersTeamAsync(int teamId);
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> CreatePlayerAsync(Player player);
        Task UpdatePlayerAsync(Player player);
        Task DeletePlayerAsync(int id);

    }
}
