using Explorer.Blog.Core.Domain.Posts;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IPostRepository: ICrudRepository<Post>
    {
        PagedResult<Post> GetPaged(int page, int pageSize);
        Post Get(long id);
        Post Create(Post entity);
        Post Update(Post entity);
        void Delete(long id);
    }
}
