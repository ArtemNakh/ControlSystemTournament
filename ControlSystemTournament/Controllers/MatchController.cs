using ControlSystemTournament.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ControlSystemTournament.Controllers
{
    
    [ApiController]
    [Route("api/[Match]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        //{
        //    var matches = await _matchService.);
        //    return Ok(matches);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Match>> GetMatch(int id)
        //{
        //    var match = await _matchService.GetMatchByIdAsync(id);
        //    if (match == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(match);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Match>> CreateMatch(Match match)
        //{
        //    var createdMatch = await _matchService.CreateMatchAsync(match);
        //    return CreatedAtAction(nameof(GetMatch), new { id = createdMatch.Id }, createdMatch);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateMatch(int id, Match match)
        //{
        //    if (id != match.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _matchService.UpdateMatchAsync(match);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMatch(int id)
        //{
        //    await _matchService.DeleteMatchAsync(id);
        //    return NoContent();
        //}
    }
}
