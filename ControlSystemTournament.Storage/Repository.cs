﻿
using System.Linq.Expressions;
using ControlSystemTournament.Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ControlSystemTournament.Storage
{
    public class Repository : IRepository
    {
        private readonly TournamentContext _context;

        public Repository(TournamentContext context)
        {
            _context = context;
        }

        public async Task<T> Add<T>(T entity) where T : class
        {
            var obj = _context.Add(entity);
            await _context.SaveChangesAsync();
            return obj.Entity;
        }

        public async Task<T> Update<T>(T entity) where T : class
        {
            var newEntity = _context.Update(entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<T> Delete<T>(int id) where T : class
        {
            var entity = await GetById<T>(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }



        public async Task<T> GetById<T>(int id) where T : class
        {
            var entity= await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            var entity = await _context.Set<T>().ToListAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetQuery<T>(Expression<Func<T, bool>> func) where T : class
        {
            var entity = await _context.Set<T>().Where(func).ToListAsync();
            return entity;
        }
    }
}
