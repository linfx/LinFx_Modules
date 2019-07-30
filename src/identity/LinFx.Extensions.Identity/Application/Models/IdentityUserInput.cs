using LinFx.Application.Models;

namespace LinFx.Extensions.Identity.Application.Models
{
    public class IdentityUserInput : PagedAndSortedResultRequest
    {
        public string Filter { get; set; }
    }
}
