using AutoMapper;
using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemTournament.Controllers
{
    //зробити повністью контроллер location(доробити put  та інщі перевірки)
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        public LocationsController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        //todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            var locations = await _locationService.GetAllLocations();
            
            var locationDTO = _mapper.Map<IEnumerable<LocationDTO>>(locations);
            return Ok(locationDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            var locationDTO = _mapper.Map<LocationDTO>(location);
            return Ok(locationDTO);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDTO>> CreateLocation(LocationDTO locationDTO)
        {
            try
            {
                var location=_mapper.Map<Location>(locationDTO);
                await _locationService.CreateLocationAsync(location);
                return Created("Created location", locationDTO);
            }
            catch (Exception)
            {
                return BadRequest("Location with this name already exists");
                throw;
            } 
          
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<LocationDTO>> UpdateLocation(int id, LocationDTO locationDTO)
        {
            if (locationDTO ==null)
            {
                return BadRequest();
            }

            var location = _mapper.Map<Location>(locationDTO);
            location.Id = id;
            await _locationService.UpdateLocationAsync(location);
            return Ok(locationDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }
    }
}
