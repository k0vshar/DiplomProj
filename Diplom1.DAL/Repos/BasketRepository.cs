using Diplom.DAL.Interfaces;
using Diplom.Domain;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly AppDBContext _context;

        public BasketRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task Create(Basket entity)
        {
            await _context.Baskets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Basket entity)
        {
            _context.Baskets.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Basket> GetAll()
        {
            return _context.Baskets;
        }

        public async Task<Basket> Update(Basket entity)
        {
            _context.Baskets.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
