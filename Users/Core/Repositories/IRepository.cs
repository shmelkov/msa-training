﻿namespace Users.Core.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<int> GetCountAsync();
	}
}
