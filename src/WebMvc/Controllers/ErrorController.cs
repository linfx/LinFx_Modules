using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error() => View();
    }
}
