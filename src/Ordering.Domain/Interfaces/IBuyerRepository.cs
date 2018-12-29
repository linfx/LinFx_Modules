using LinFx.Domain;
using Ordering.Domain.Models;
using System.Threading.Tasks;

namespace Ordering.Domain.Interfaces
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);
        Buyer Update(Buyer buyer);
        Task<Buyer> FindAsync(string identity);
        Task<Buyer> FindByIdAsync(int id);
    }
}
