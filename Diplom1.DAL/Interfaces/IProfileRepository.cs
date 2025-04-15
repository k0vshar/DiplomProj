// Interfaces/IGoodRepository.cs
using Diplom.Domain;

namespace Diplom.DAL.Interfaces
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAllAsync();
        Task<Profile?> GetByIdAsync(int id);
        Task AddAsync(Profile profile);
        Task UpdateAsync(Profile profile);
        Task DeleteAsync(int id);
    }
}
