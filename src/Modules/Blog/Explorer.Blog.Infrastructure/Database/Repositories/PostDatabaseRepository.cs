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
    public class PostDatabaseRepository : ICrudRepository<Post>, IPostRepository
    {
        private readonly BlogContext _blogContext;
        
        public PostDatabaseRepository(BlogContext context)
        {
            _blogContext = context;
        }
        public Post Create(Post post)
        {
            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
            return post;
        }

        public void Delete(long id)
        {
            var post = Get(id);
            _blogContext.Posts.Remove(post);
            _blogContext.SaveChanges();
        }

        public Post Get(long id)
        {
            Post? post= _blogContext.Posts.Where(b => b.Id == id)
           .Include(b => b.Comments!).FirstOrDefault();
            return post == null ? throw new KeyNotFoundException("Not found: " +id) : post;
        }

        public PagedResult<Post> GetPaged(int page, int pageSize)
        {
            var task = _blogContext.Posts.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Post Update(Post post)
        {
            _blogContext.Entry(post).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return post;
        }
    }
}
