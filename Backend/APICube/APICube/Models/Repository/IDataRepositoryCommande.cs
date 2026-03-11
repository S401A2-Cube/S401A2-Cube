using APICube.Models.EntityFramework;
using static APICube.Models.Repository.IDataRepository;

namespace APICube.Models.Repository
{
    public interface IDataRepositoryCommande : IDataRepository<Commande>
    {
    }
}
