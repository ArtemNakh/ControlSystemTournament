using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository _context;

        public MatchService(IRepository context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Match>> GetAllMatchesAsync()
        //{
        //    return await _context.Matches.Include(m => m.TeamA)
        //                                 .Include(m => m.TeamB)
        //                                 .Include(m => m.Tournament)
        //                                 .ToListAsync();
        //}

        public async Task<Match> GetMatchByIdAsync(int id)
        {
            return await _context.GetById<Match>(id);
        }


        //todo(create new object and check value)
        public async Task<Match> CreateMatchAsync(Match match)
        {
            await _context.Add(match);
            return match;
        }

        public async Task UpdateMatchAsync(Match match)
        {
            await _context.Update(match);
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _context.GetById<Match>(id); 
            if (match != null)
                await _context.Delete<Match>(id);
            
        }

        //public async Task<IEnumerable<Team>> GetTeamsMatcheAsync(Match match)
        //{
        //    // Перевірка, що об'єкт матчу не є null
        //    if (match == null)
        //    {
        //        throw new ArgumentNullException(nameof(match), "Match cannot be null");
        //    }

        //    // Створення колекції для зберігання команд
        //    var teams = new List<Team>
        //    {
        //        // Додавання команд у колекцію, якщо вони існують

        //        match.TeamA,
        //        match.TeamB
        //    };
            

        //    // Повернення колекції команд
        //    return  teams;
        //}
    }
}
