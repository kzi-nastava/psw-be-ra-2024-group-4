using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class PostDatabaseRepository : CrudDatabaseRepository<Post,BlogContext>, IPostRepository //nasledjujemo i implementaciju osnovnih crud a i redefinisemo te koje su nama potrebne tj cija smo zaglavlja def u IPostRepo-u
    {   
        public PostDatabaseRepository(BlogContext context): base(context) { } //u crud database se context se koristi kao DbContext

        public new Post Get(long id)
        {
            Post? post= DbContext.Posts.Where(b => b.Id == id)
           .Include(b => b.Comments!).FirstOrDefault();
            return post == null ? throw new KeyNotFoundException("Not found: " +id) : post;
        }
        public new Post Update(Post post)
        {
            DbContext.Entry(post).State = EntityState.Modified;
            DbContext.SaveChanges();
            return post;
        }
    }
}
