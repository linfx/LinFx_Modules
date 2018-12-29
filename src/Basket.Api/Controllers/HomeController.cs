using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("swagger");
        }
    }
}
