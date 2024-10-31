using Explorer.Blog.Core.Domain.Posts;
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
        new Post Update(Post post); //new znaci da ce gledati nove implementacije ovih metoda a ne ove izvorne iz ICrudRepository-a tj. one su implementirane u CrudDatabaseRepo-u
        new Post Get(long id);
    }
}
