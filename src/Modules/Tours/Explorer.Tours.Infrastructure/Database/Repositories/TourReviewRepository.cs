using Explorer.Modules.Core.Domain;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourReviewRepository
    {
        private readonly ToursContext _context;

        public TourReviewRepository(ToursContext context)
        {
            _context = context;
        }

        public List<TourReview> GetByTouristId(long touristId)
        {
            return _context.TourReview.Where(tr => tr.IdTourist == touristId).ToList();
        }
        public List<TourReview> GetByTourId(long tourId)
        {
            return _context.TourReview.Where(tr => tr.IdTour == tourId).ToList();
        }
    }
}
