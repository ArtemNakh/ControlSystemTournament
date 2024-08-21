using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository _context;

        public PlayerService(IRepository context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayersTournamentAsync(int tournamentId)
        {
            if (tournamentId == null)
            {
                throw new ArgumentNullException(nameof(tournamentId), "Tournament cannot be null");
            }
            return await _context.GetQuery<Player>(p => p.Team.Tournament.Id == tournamentId);
        }

        public async Task<IEnumerable<Player>> GetPlayersTeamAsync(int teamId)
        {
            if (teamId == null)
            {
                throw new ArgumentNullException(nameof(teamId), "Team cannot be null");
            }

            return await _context.GetQuery<Player>(p => p.Team.Id == teamId);
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await _context.GetById<Player>(id);
        }

        public async Task<Player> CreatePlayerAsync(Player player)
        {
            await _context.Add(player);
            return player;
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            await _context.Update(player);
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await _context.GetById<Player>(id);
            if (player != null)
            {
                await _context.Delete<Player>(id);
            }
        }

    }
}
