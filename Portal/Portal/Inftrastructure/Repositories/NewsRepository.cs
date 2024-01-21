using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Repositories;

namespace Portal.Inftrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private NewsContext _context;
        public NewsRepository(NewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.Set<News>().ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.Set<News>().FindAsync(id);
        }

        public async Task<News> AddAsync(News entity)
        {
            await _context.Set<News>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(News entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(News entity)
        {
            _context.Set<News>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
