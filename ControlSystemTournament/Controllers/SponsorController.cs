using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{
    [Route("api/Sponsor")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _sponsorService;

        public SponsorController(ISponsorService sponsorService)
        {
            _sponsorService = sponsorService;
        }

        // GET: api/sponsors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSponsorById(int id)
        {
            var sponsor = await _sponsorService.GetSponsorByIdAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return Ok(sponsor);
        }

        // POST: api/sponsors
        [HttpPost]
        public async Task<IActionResult> CreateSponsor([FromBody] Sponsor sponsor)
        {
            if (sponsor == null)
            {
                return BadRequest();
            }

            var createdSponsor = await _sponsorService.CreateSponsorAsync(sponsor);
            return Created();
        }

        // PUT: api/sponsors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSponsor(int id, [FromBody] Sponsor sponsor)
        {
            if (sponsor == null || sponsor.Id != id)
            {
                return BadRequest();
            }

            var existingSponsor = await _sponsorService.GetSponsorByIdAsync(id);
            if (existingSponsor == null)
            {
                return NotFound();
            }

            await _sponsorService.UpdateSponsorAsync(sponsor);
            return NoContent();
        }

        // DELETE: api/sponsors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsor(int id)
        {
            var sponsor = await _sponsorService.GetSponsorByIdAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            await _sponsorService.DeleteSponsorAsync(id);
            return NoContent();
        }

        // GET: api/sponsors/tournament/{tournamentId}
        [HttpGet("tournament/{tournamentId}")]
        public async Task<IActionResult> GetSponsorsByTournament(int tournamentId)
        {
            var tournament = new Tournament { Id = tournamentId }; // Assuming you have a method to get a Tournament object by Id
            var sponsors = await _sponsorService.GetAllSponsorsTournamentAsync(tournament);

            return Ok(sponsors);
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Sponsor>>> GetSponsors()
        //{
        //    var sponsors = await _sponsorService.GetAllSponsorsAsync();
        //    return Ok(sponsors);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Sponsor>> GetSponsor(int id)
        //{
        //    var sponsor = await _sponsorService.GetSponsorByIdAsync(id);
        //    if (sponsor == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(sponsor);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Sponsor>> CreateSponsor(Sponsor sponsor)
        //{
        //    var createdSponsor = await _sponsorService.CreateSponsorAsync(sponsor);
        //    return CreatedAtAction(nameof(GetSponsor), new { id = createdSponsor.Id }, createdSponsor);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateSponsor(int id, Sponsor sponsor)
        //{
        //    if (id != sponsor.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _sponsorService.UpdateSponsorAsync(sponsor);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSponsor(int id)
        //{
        //    await _sponsorService.DeleteSponsorAsync(id);
        //    return NoContent();
        //}
    }
}
