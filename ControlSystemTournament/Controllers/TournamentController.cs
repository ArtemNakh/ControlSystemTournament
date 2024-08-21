using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{
    [Route("api/Tournament")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            var tournaments = await _tournamentService.GetAllTournamentsAsync();
            return Ok(tournaments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] Tournament tournament)
        {
            if (tournament == null)
            {
                return BadRequest();
            }

            var createdTournament = await _tournamentService.CreateTournamentAsync(tournament);
            return Created();
        }

        // PUT: api/tournaments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] Tournament tournament)
        {
            if (tournament == null || tournament.Id != id)
            {
                return BadRequest();
            }

            var existingTournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (existingTournament == null)
            {
                return NotFound();
            }

            await _tournamentService.UpdateTournamentAsync(tournament);
            return NoContent();
        }

        // DELETE: api/tournaments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            await _tournamentService.DeleteTournamentAsync(id);
            return NoContent();
        }
       
    }
}
