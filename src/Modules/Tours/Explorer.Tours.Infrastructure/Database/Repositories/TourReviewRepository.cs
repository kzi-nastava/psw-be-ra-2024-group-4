using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourReviewRepository : ITourReviewRepository
    {
        private readonly ToursContext _dbContext;

        public TourReviewRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<TourReview> GetByTourId(long tourId, int page, int pageSize)
        {
            var totalReviews = _dbContext.TourReview.Count(tr => tr.IdTour == tourId);

            var totalPages = (int)Math.Ceiling(totalReviews / (double)pageSize);

            var query = _dbContext.TourReview
                .Where(tr => tr.IdTour == tourId)
                .OrderBy(tr => tr.DateComment)
                .Skip((page - 1) * pageSize);


            if (pageSize > 0) {
                query = query
                    .Take(pageSize);
            }

            var reviews = query.ToList();

            return new PagedResult<TourReview>(reviews, reviews.Count());
        }
    }
}
