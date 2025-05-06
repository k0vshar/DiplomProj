// Repos/GoodRepository.cs
using System;
using Diplom.DAL.Interfaces;
using Diplom.Domain;
using Diplom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL.Repos
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly AppDBContext _context;

        public ProfileRepository(AppDBContext context)
        {
            _context = context;
        }

        public IQueryable<Profile> GetAll()
        {
            return _context.Profiles;
        }
        public async Task<Profile?> GetByIdAsync(int id) =>
            await _context.Profiles.FindAsync(id);

        public async Task Create(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<Profile> Update(Profile entity)
        {
            _context.Profiles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Profile entity)
        {
            _context.Profiles.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
