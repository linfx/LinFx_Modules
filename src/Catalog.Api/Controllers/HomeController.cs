using Microsoft.AspNetCore.Mvc;

namespace ShopFx.Catalog.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("swagger");
        }
    }
}