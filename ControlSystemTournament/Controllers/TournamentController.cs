using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{
    [Route("api/[controller]")]
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

     
        //[HttpPost]
        //public async Task<ActionResult<Tournament>> CreateTournament(string nameTournament,string description,DateTime startDate,DateTime endDate,deciaml prizePool,/*Tournament tournament*/)
        //{
        //    var createdTournament = await _tournamentService.CreateTournamentAsync(tournament);
        //    return CreatedAtAction(nameof(GetTournament), new { id = createdTournament.Id }, createdTournament);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTournament(int id, Tournament tournament)
        //{
        //    if (id != tournament.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _tournamentService.UpdateTournamentAsync(tournament);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTournament(int id)
        //{
        //    await _tournamentService.DeleteTournamentAsync(id);
        //    return NoContent();
        //}
    }
}
