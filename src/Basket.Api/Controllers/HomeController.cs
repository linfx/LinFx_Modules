using Microsoft.AspNetCore.Mvc;

namespace ShopFx.Basket.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return new RedirectResult("swagger");
        }
    }
}
