// Repos/GoodRepository.cs
using System;
using Diplom.DAL.Interfaces;
using Diplom.Domain;
using Diplom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL.Repos
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDBContext _context;

        public ProfileRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profile>> GetAllAsync() =>
            await _context.Profiles.ToListAsync();

        public async Task<Profile?> GetByIdAsync(int id) =>
            await _context.Profiles.FindAsync(id);

        public async Task AddAsync(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profile profile)
        {
            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }
    }
}
