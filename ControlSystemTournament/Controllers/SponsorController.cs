//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ControlSystemTournament.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SponsorController : ControllerBase
//    {
//        private readonly ISponsorService _sponsorService;

//        public SponsorController(ISponsorService sponsorService)
//        {
//            _sponsorService = sponsorService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Sponsor>>> GetSponsors()
//        {
//            var sponsors = await _sponsorService.GetAllSponsorsAsync();
//            return Ok(sponsors);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Sponsor>> GetSponsor(int id)
//        {
//            var sponsor = await _sponsorService.GetSponsorByIdAsync(id);
//            if (sponsor == null)
//            {
//                return NotFound();
//            }
//            return Ok(sponsor);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Sponsor>> CreateSponsor(Sponsor sponsor)
//        {
//            var createdSponsor = await _sponsorService.CreateSponsorAsync(sponsor);
//            return CreatedAtAction(nameof(GetSponsor), new { id = createdSponsor.Id }, createdSponsor);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateSponsor(int id, Sponsor sponsor)
//        {
//            if (id != sponsor.Id)
//            {
//                return BadRequest();
//            }

//            await _sponsorService.UpdateSponsorAsync(sponsor);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteSponsor(int id)
//        {
//            await _sponsorService.DeleteSponsorAsync(id);
//            return NoContent();
//        }
//    }
//}
