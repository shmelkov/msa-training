using Portal.Core.Entities;

namespace Portal.Core.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News> GetByIdAsync(int id);
        Task<News> AddAsync(News entity);
        Task UpdateAsync(News entity);
        Task DeleteAsync(News entity);
    }
}
