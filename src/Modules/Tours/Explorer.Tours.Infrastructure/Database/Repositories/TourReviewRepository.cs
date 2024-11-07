using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourReviewRepository : ITourReviewRepository
    {
        private readonly ToursContext _context;

        public TourReviewRepository(ToursContext context)
        {
            _context = context;
        }

        public PagedResult<TourReview> GetByTouristId(long touristId)
        {

            var reviews = _context.TourReview.Where(tr => tr.IdTourist == touristId).ToList();
            var pagedResult = new PagedResult<TourReview>(reviews, reviews.Count);
            return pagedResult;
        }
        public PagedResult<TourReview> GetByTourId(long tourId)
        {
            var reviews = _context.TourReview.Where(tr => tr.IdTour == tourId).ToList();
            var pagedResult = new PagedResult<TourReview>(reviews, reviews.Count);
            return pagedResult;
        }
    }
}
