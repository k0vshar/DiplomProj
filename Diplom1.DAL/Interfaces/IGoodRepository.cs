using Diplom.Domain.Entities;

namespace Diplom.DAL.Interfaces
{
    public interface IGoodRepository
    {
        Task<IEnumerable<Good>> GetAllAsync();
        Task<Good?> GetByIdAsync(int id);
        Task AddAsync(Good good);
        Task UpdateAsync(Good good);
        Task DeleteAsync(int id);
    }
}

