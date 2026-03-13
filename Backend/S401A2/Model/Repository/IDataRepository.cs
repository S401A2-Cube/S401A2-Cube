using Microsoft.AspNetCore.Mvc;

namespace S401A2.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}