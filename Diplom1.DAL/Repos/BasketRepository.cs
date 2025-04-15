// Repos/GoodRepository.cs
using System;
using Diplom.DAL.Interfaces;
using Diplom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL.Repos
{
    public class BasketRepository : IBasketRepository
    {
        private readonly AppDBContext _context;

        public BasketRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Basket>> GetAllAsync() =>
            await _context.Baskets.ToListAsync();

        public async Task<Basket?> GetByIdAsync(int id) =>
            await _context.Baskets.FindAsync(id);

        public async Task AddAsync(Basket basket)
        {
            await _context.Baskets.AddAsync(basket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Basket basket)
        {
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var basket = await _context.Baskets.FindAsync(id);
            if (basket != null)
            {
                _context.Baskets.Remove(basket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
