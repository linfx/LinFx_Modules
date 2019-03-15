using LinFx.Blogging.Domain.Models;

namespace LinFx.Blogging.Application.Services
{
    public class PostWithDetails : Post
    {
        public int CommentCount { get; set; }
    }
}