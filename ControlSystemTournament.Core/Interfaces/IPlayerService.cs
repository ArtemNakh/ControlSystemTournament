using ControlSystemTournament.Core.Models;


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
