using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;


namespace ControlSystemTournament.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository _context;

        public TeamService(IRepository context)
        {
            _context = context;
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _context.GetById<Team>(id);
        }

        public async Task<Team> CreateTeamAsync(Team team)
        {
            await _context.Add(team);
            return team;
        }

        public async Task UpdateTeamAsync(Team team)
        {
           await _context.Update(team);
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _context.GetById<Team>(id);
            if (team != null)
            {
                await _context.Delete<Team>(id);
            }
        }

        public Task<IEnumerable<Team>> GetTeamsTournamentAsync(Tournament tournament)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament), "Tournament cannot be null");
            }

           
            return  _context.GetQuery<Team>(t => t.Tournament.Id == tournament.Id);
        }
    }
}
