using APICube.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace APICube.Models.Repository
{
    public interface IDataRepositoryVelo : IDataRepository<Velo>
    {
        Task<ActionResult<IEnumerable<Velo>>> GetByMateriauID(int id);
        Task<ActionResult<IEnumerable<Velo>>> GetByCouleurID(int id);
        Task<ActionResult<IEnumerable<Velo>>> GetByTailleID(int id);
        Task<ActionResult<IEnumerable<Velo>>> GetByModeleID(int id);
        Task<ActionResult<IEnumerable<Velo>>> GetByMillesimeID(int id);
    }
}
