using Explorer.Blog.Core.Domain.Posts;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IPostRepository
    {
        PagedResult<Post> GetPaged(int page, int pageSize);
        Post Get(long id);
        Post Create(Post entity);
        Post Update(Post entity);
        void Delete(long id);
    }
}
