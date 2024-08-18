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

        //public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        //{
        //    return await _context.Players.ToListAsync();
        //}

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

        public Task<IEnumerable<Player>> GetPlayersTournamentAsync(Tournament tournament)
        {
            if (tournament == null)
            {
            //    throw new ArgumentNullException(nameof(tournament), "Tournament cannot be null");
            }
            throw new ArgumentNullException(nameof(tournament), "Tournament cannot be null");
         //   return _context.GetQuery<Player>(p => p.Team.Tournament.Id == tournament.Id);
        }

        public Task<IEnumerable<Player>> GetPlayersTeamAsync(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team), "Team cannot be null");
            }

            return  _context.GetQuery<Player>(p => p.Id == team.Id);
        }
    }
}
