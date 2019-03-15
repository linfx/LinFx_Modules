using LinFx.Application.Models;
using LinFx.Blogging.Domain.Models;
using LinFx.Blogging.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LinFx.Blogging.Application.Services
{
    public class BlogService
    {
        protected BloggingDbContext _context;

        public BlogService(BloggingDbContext context)
        {
            _context = context;
        }

        public Task<Blog> GetAsync(Guid id)
        {
            return _context.Blogs.FindAsync(id);
        }

        public async Task<ListResult<Blog>> GetListAsync()
        {
            var items = await _context.Blogs.ToListAsync();
            return new ListResult<Blog>(items);
        }
    }
}
