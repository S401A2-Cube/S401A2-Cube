using Microsoft.AspNetCore.Mvc;

namespace S401A2.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}