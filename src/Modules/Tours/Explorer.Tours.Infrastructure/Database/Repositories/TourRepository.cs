using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ToursContext _dbContext;

        public TourRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Tour> GetToursByUserId(long userId)
        {
            return _dbContext.Tour
                         .Where(t => t.UserId == userId)
                         .ToList();
        }

        public Tour GetSpecificTourByUser(long tourId, long userId)
        {
            return _dbContext.Tour.SingleOrDefault(t => t.Id == tourId && t.UserId == userId);
        }
    }
}
