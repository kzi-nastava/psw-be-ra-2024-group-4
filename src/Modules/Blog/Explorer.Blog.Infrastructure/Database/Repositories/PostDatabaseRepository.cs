using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.Domain;
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
    public class PostDatabaseRepository : IPostRepository
    {   
        private readonly BlogContext _blogContext;
        public PostDatabaseRepository(BlogContext blogContext)
        { 
            _blogContext = blogContext;
        }

        public PagedResult<Post> GetPaged(int page, int pageSize)
        {
            var task = _blogContext.Posts.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Post Create(Post entity)
        {
            _blogContext.Posts.Add(entity);
            _blogContext.SaveChanges();
            return entity;
        }

        public Post Get(long id)
        {
            Post? post= _blogContext.Posts.Where(b => b.Id == id)
           .Include(b => b.Comments!).FirstOrDefault();
            return post == null ? throw new KeyNotFoundException("Not found: " +id) : post;
        }
        public Post Update(Post post)
        {
            _blogContext.Entry(post).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return post;
        }

        public void Delete(long id)
        {
            var entity = Get(id);
            _blogContext.Posts.Remove(entity);
            _blogContext.SaveChanges();
        }
    }
}
