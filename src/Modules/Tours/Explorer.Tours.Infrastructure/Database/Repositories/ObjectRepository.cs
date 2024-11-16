using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly ToursContext _dbContext;

        public ObjectRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Core.Domain.Object> GetAll()
        {
            return _dbContext.Objects.ToList();
        }
    }
}
