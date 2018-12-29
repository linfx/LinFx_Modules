using Microsoft.AspNetCore.Mvc;

namespace Ordering.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("swagger");
        }
    }
}