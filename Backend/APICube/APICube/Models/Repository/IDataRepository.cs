using Microsoft.AspNetCore.Mvc;

namespace APICube.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int idmateriau);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
