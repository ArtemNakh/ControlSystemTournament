﻿using ControlSystemTournament.Core.Models;
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
            return await _context.GetById<Location>(id);
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
            var location = await _context.GetById<Location>(id);
            if (location != null)
            {
                await _context.Delete<Location>(id);
            }
        }

        public Task<IEnumerable<Location>> GetAllLocations()
        {
            var locations = (IEnumerable<Location>)_context.GetAll<Location>();
            return Task.FromResult(locations);
        }
    }
}
