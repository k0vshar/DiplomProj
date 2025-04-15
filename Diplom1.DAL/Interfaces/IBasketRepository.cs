// Interfaces/IGoodRepository.cs
using Diplom.Domain.Entities;

namespace Diplom.DAL.Interfaces
{
    public interface IBasketRepository
    {
        Task<IEnumerable<Basket>> GetAllAsync();
        Task<Basket?> GetByIdAsync(int id);
        Task AddAsync(Basket basket);
        Task UpdateAsync(Basket basket);
        Task DeleteAsync(int id);
    }
}

