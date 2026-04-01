using APICube.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S401A2.Models.Repository
{
    public interface IVeloRepository : IDataRepository<Velo>
    {
        Task<IEnumerable<Velo>> GetChunkAsync(int skip, int take);
    }
}