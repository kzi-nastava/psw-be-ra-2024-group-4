using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        PagedResult<TourReview> GetByTouristId(long touristId);
        PagedResult<TourReview> GetByTourId(long tourId);
    }
}
