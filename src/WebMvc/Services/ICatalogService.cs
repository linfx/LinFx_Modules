using Microsoft.AspNetCore.Mvc.Rendering;
using WebMvc.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebMvc.Services
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetCatalogItem(int id);
        Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type);
        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
