using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{

    [ApiController]
    [Route("api/Match")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        // GET: api/matches/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatchById(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }

        // POST: api/matches
        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] Match match)
        {
            if (match == null)
            {
                return BadRequest();
            }

            var createdMatch = await _matchService.CreateMatchAsync(match);
            return Created();
        }

        // PUT: api/matches/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, [FromBody] Match match)
        {
            if (match == null || match.Id != id)
            {
                return BadRequest();
            }

            var existingMatch = await _matchService.GetMatchByIdAsync(id);
            if (existingMatch == null)
            {
                return NotFound();
            }

            await _matchService.UpdateMatchAsync(match);
            return NoContent();
        }

        // DELETE: api/matches/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            await _matchService.DeleteMatchAsync(id);
            return NoContent();
        }

        // GET: api/matches/tournament/{tournamentId}
        [HttpGet("tournament/{tournamentId}")]
        public async Task<IActionResult> GetMatchesByTournament(int tournamentId)
        {
            var tournament = new Tournament { Id = tournamentId }; // Assuming you have a method to get a Tournament object by Id
            var matches = await _matchService.GetAllMatchesTournamentAsync(tournament);

            return Ok(matches);
        }

    }
}
