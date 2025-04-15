using System;
using Diplom.DAL.Interfaces;
using Diplom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL.Repos
{
    public class GoodRepository : IGoodRepository
    {
        private readonly AppDBContext _context;

        public GoodRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Good>> GetAllAsync() =>
            await _context.Goods.ToListAsync();

        public async Task<Good?> GetByIdAsync(int id) =>
            await _context.Goods.FindAsync(id);

        public async Task AddAsync(Good good)
        {
            await _context.Goods.AddAsync(good);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Good good)
        {
            _context.Goods.Update(good);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var good = await _context.Goods.FindAsync(id);
            if (good != null)
            {
                _context.Goods.Remove(good);
                await _context.SaveChangesAsync();
            }
        }
    }
}
