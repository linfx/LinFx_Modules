using LinFx.Blogging.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LinFx.Blogging.EntityFrameworkCore
{
    public class BloggingDbContext : Extensions.EntityFrameworkCore.DbContext
    {
        public DbSet<BlogUser> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogUser>(b =>
            {
            });

            modelBuilder.Entity<Blog>(b =>
            {
                b.Property(x => x.Name).IsRequired().HasMaxLength(200).HasColumnName(nameof(Blog.Name));
                b.Property(x => x.ShortName).IsRequired().HasMaxLength(100).HasColumnName(nameof(Blog.ShortName));
                b.Property(x => x.Description).IsRequired(false).HasMaxLength(2000).HasColumnName(nameof(Blog.Description));
            });
        }
    }
}
