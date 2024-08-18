//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ControlSystemTournament.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TeamController : ControllerBase
//    {
//        private readonly ITeamService _teamService;

//        public TeamController(ITeamService teamService)
//        {
//            _teamService = teamService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
//        {
//            var teams = await _teamService.GetAllTeamsAsync();
//            return Ok(teams);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Team>> GetTeam(int id)
//        {
//            var team = await _teamService.GetTeamByIdAsync(id);
//            if (team == null)
//            {
//                return NotFound();
//            }
//            return Ok(team);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Team>> CreateTeam(Team team)
//        {
//            var createdTeam = await _teamService.CreateTeamAsync(team);
//            return CreatedAtAction(nameof(GetTeam), new { id = createdTeam.Id }, createdTeam);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateTeam(int id, Team team)
//        {
//            if (id != team.Id)
//            {
//                return BadRequest();
//            }

//            await _teamService.UpdateTeamAsync(team);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTeam(int id)
//        {
//            await _teamService.DeleteTeamAsync(id);
//            return NoContent();
//        }
//    }
//}
