using Microsoft.EntityFrameworkCore;
using Users.Core.Repositories;

namespace Users.Infrastructure.Repositories
{
	public class BaseRepository<T, K> : IRepository<T>
		   where T : class
		   where K : DbContext
	{
		protected readonly K _context;
		public BaseRepository(K context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id) ?? throw new ArgumentException("Not found!");

        public async Task<T> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id) ?? throw new ArgumentException("Not found!");

        public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task<int> GetCountAsync() => await _context.Set<T>().CountAsync();
	}
}
