using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
        ConfigureBlog(modelBuilder);
    }

    private static void ConfigureBlog(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
                    .HasMany(p => p.Comments)
                    .WithOne()
                    .HasForeignKey(c => c.PostId);
        modelBuilder.Entity<Post>()
                     .Property(p=>p.Ratings).HasColumnType("jsonb");
    }

}