using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
