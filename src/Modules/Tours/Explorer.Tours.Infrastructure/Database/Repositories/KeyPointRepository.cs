using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class KeyPointRepository : IKeyPointRepository
    {
        private readonly ToursContext _dbContext;

        public KeyPointRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<KeyPoint> GetKeyPointsByUserId(long userId)
        {
         
            
            return _dbContext.KeyPoints
                         .Where(kp => kp.UserId == userId)
                         .ToList();
        }

        public int GetMaxId(long userId)
        {
            return _dbContext.KeyPoints.Max(kp => (int?)kp.Id) ?? 0;
        }

        public List<KeyPoint> GetAll()
        {
            return _dbContext.KeyPoints.ToList();
        }
    }
}
