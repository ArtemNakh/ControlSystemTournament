using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{

    [ApiController]
    [Route("api/Match")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;

        public MatchController(IMatchService matchService, IMapper mapper)
        {
            _matchService = matchService;
            _mapper = mapper;
        }

        // GET: api/matches/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDTO>> GetMatchById(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            var matchDTO = _mapper.Map<MatchDTO>(match);
            return Ok(matchDTO);
        }

        // POST: api/matches
        [HttpPost]
        public async Task<ActionResult<MatchDTO>> CreateMatch([FromBody] MatchDTO matchDTO)
        {
            if (matchDTO == null)
            {
                return BadRequest();
            }

            var match = _mapper.Map<Match>(matchDTO);
            await _matchService.CreateMatchAsync(match);

            var createdMatchDTO = _mapper.Map<MatchDTO>(match);
            return Created("Created Match",createdMatchDTO);
        }

        // PUT: api/matches/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, [FromBody] MatchDTO matchDTO)
        {
            if (matchDTO == null )
            {
                return BadRequest();
            }

            var existingMatch = await _matchService.GetMatchByIdAsync(id);
            if (existingMatch == null)
            {
                return NotFound();
            }

            var match = _mapper.Map<Match>(matchDTO);
            await _matchService.UpdateMatchAsync(match);
            return NoContent();
        }

        // DELETE: api/matches/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMatch(int id)
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
        public async Task<ActionResult<MatchDTO>> GetMatchesByTournament(int tournamentId)
        {
            var tournament = new Tournament { Id = tournamentId }; 
            var matches = await _matchService.GetAllMatchesTournamentAsync(tournament);
            var matchesDTO = _mapper.Map<MatchDTO>(matches);
            return Ok(matchesDTO);
        }

    }
}
