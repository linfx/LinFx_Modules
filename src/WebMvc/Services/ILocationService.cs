using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Services
{
    public interface ILocationService
    {
        Task CreateOrUpdateUserLocation(LocationDTO location);
    }
}
