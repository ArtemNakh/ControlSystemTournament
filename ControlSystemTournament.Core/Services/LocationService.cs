using ControlSystemTournament.Core.Models;
using ControlSystemTournament.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ControlSystemTournament.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository _context;

        public LocationService(IRepository context)
        {
            _context = context;
        }

        public async Task<Location> GetLocationByIdAsync(int id)
        {
            try
            {
                var location = await _context.GetById<Location>(id);
                if (location == null)
                {
                    throw new Exception("Location not found");
                }
                return location;
            }
            catch (Exception)
            {
                throw new Exception("Location not found");
            }
            
           
        }

       
        public async Task<Location> CreateLocationAsync(Location location)
        {
            var IsHas = await _context.GetQuery<Location>(l => l.Name == location.Name);
            if (!IsHas.IsNullOrEmpty())
            {
                throw new Exception("Location with this name already exists.");
            }

            await _context.Add(location);
           
            return location;
        }

        public async Task UpdateLocationAsync(Location location)
        {
            await _context.Update(location);
        }

        public async Task DeleteLocationAsync(int id)
        {
            
            try
            {
                await _context.Delete<Location>(id);
                await _context.GetById<Location>(id);
            }
            catch (Exception)
            {

                throw new Exception("Error Delete");
            }
           
                
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            var locations = await _context.GetAll<Location>();
            return locations;
        }
    }
}
