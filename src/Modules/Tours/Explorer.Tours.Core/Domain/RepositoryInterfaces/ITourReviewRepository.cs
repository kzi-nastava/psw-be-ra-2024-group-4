using Explorer.Modules.Core.Domain;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        List<TourReview> GetByTouristId(long touristId);
        List<TourReview> GetByTourId(long tourId);
    }
}
