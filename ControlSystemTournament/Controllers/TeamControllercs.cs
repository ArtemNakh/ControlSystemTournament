using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        //{
        //    var teams = await _teamService.GetAllTeamsAsync();
        //    return Ok(teams);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            var createdTeam = await _teamService.CreateTeamAsync(team);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
        {
            if (team == null || team.Id != id)
            {
                return BadRequest();
            }

            var existingTeam = await _teamService.GetTeamByIdAsync(id);
            if (existingTeam == null)
            {
                return NotFound();
            }

            await _teamService.UpdateTeamAsync(team);
            return NoContent();
        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTeam(int id, Team team)
        //{
        //    if (id != team.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _teamService.UpdateTeamAsync(team);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            await _teamService.DeleteTeamAsync(id);
            return NoContent();
        }

        [HttpGet("{tournamentId}")]
        public async Task<IActionResult> GetTeamsByTournament(int tournamentId)
        {
            var tournament = new Tournament { Id = tournamentId }; // Assuming you have a method to get a Tournament object by Id
            var teams = await _teamService.GetTeamsTournamentAsync(tournament);

            return Ok(teams);
        }
    }
}
