using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ControlSystemTournament.Controllers
{
    [Route("api/Tournament")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public TournamentController(ITournamentService tournamentService, IMapper mapper, ILocationService locationService)
        {
            _tournamentService = tournamentService;
            _mapper = mapper;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetTournaments()
        {
            var tournaments = await _tournamentService.GetAllTournamentsAsync();
            var tournamentDTOs = _mapper.Map<IEnumerable<TournamentDTO>>(tournaments);
            return Ok(tournamentDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDTO>> GetTournament(int id)
        {
            var tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            var tournamentDTO = _mapper.Map<TournamentDTO>(tournament);
            return Ok(tournamentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTournament([FromBody] TournamentDTO tournamentDTO)
        {
            if (tournamentDTO == null)
            {
                return BadRequest();
            }

            var tournament = _mapper.Map<Tournament>(tournamentDTO);
            tournament.Location = _locationService.GetLocationByIdAsync(tournamentDTO.LocationId).Result;
            var createdTournament = await _tournamentService.CreateTournamentAsync(tournament);
            return Created();
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdatePrizePoolTournament(int id, [FromBody] int NewPrizePool)
        //{
        //    if ( NewPrizePool==null)
        //    {
        //        return BadRequest("Prize pool are empty .");
        //    }

        //    var existingTournament = await _tournamentService.GetTournamentByIdAsync(id);
        //    if (existingTournament == null)
        //    {
        //        return NotFound("Tournament not found.");
        //    }

        //    // Оновлення властивостей існуючого турніру
        //    existingTournament.PrizePool = tournamentDTO.NameTournament;
           

        //    // Оновлення турніру в базі даних
        //    try
        //    {
        //        await _tournamentService.UpdateTournamentAsync(existingTournament);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }

        //    return Ok("Tournament updated successfully.");
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateTournament(int id, [FromBody] TournamentDTO tournamentDTO)
        //{
        //    if (tournamentDTO == null || string.IsNullOrEmpty(tournamentDTO.NameTournament))
        //    {
        //        return BadRequest("Tournament data is invalid.");
        //    }

        //    // Отримання існуючого турніру
        //    var existingTournament = await _tournamentService.GetTournamentByIdAsync(id);
        //    if (existingTournament == null)
        //    {
        //        return NotFound("Tournament not found.");
        //    }

        //    // Отримання нової локації
        //    var location = await _locationService.GetLocationByIdAsync(tournamentDTO.LocationId);
        //    if (location == null)
        //    {
        //        return BadRequest("Location not found.");
        //    }

        //    // Оновлення властивостей існуючого турніру
        //    existingTournament.NameTournament = tournamentDTO.NameTournament;
        //    existingTournament.StartDate = tournamentDTO.StartDate; 
        //    existingTournament.EndDate = tournamentDTO.EndDate; 
        //    existingTournament.Description = tournamentDTO.Description; 
        //    existingTournament.StartDate = tournamentDTO.StartDate; 
        //    existingTournament.Location = location;

        //    // Оновлення турніру в базі даних
        //    try
        //    {
        //        await _tournamentService.UpdateTournamentAsync(existingTournament);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }

        //    return Ok("Tournament updated successfully.");
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(int id)
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
