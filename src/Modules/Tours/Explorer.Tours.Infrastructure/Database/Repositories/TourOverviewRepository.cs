using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourOverviewRepository : ITourOverviewRepository
    {
        private readonly ToursContext _dbContext;

        public TourOverviewRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<TourReview> GetAllByTourId(int page, int pageSize, long tourId)
        {
            var totalCount = _dbContext.TourReview
            .Count(tr => tr.IdTour == tourId);

            var results = _dbContext.TourReview
                .Where(tr => tr.IdTour == tourId)
                .OrderBy(tr => tr.DateComment)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pagedResult = new PagedResult<TourReview>(results, totalCount);

            return pagedResult;
        }

        public PagedResult<TourOverview> GetAllWithoutReviews(int page, int pageSize)
        {
            //var totalCount = _dbContext.TourExecution
            //    .Where(te => te.Status == TourExecutionStatus.Active)
            //    .Select(te => te.TourId)
            //    .Distinct()
            //    .Count();

            //var activeTours = _dbContext.Tour
            //    .Where(t => _dbContext.TourExecution.Any(te =>
            //        te.TourId == t.Id && te.Status == TourExecutionStatus.Active))
            //    .OrderBy(t => t.PublishedTime)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToList();

            var activeTours = _dbContext.Tour.ToList();
            var totalCount = activeTours.Count();

            var tours = activeTours.Select(t =>
            {
                var keyPointId = t.KeyPointIds.FirstOrDefault();

                var firstKeyPoint = _dbContext.KeyPoints
                    .Where(kp => kp.Id == keyPointId)
                    .FirstOrDefault();

                return new TourOverview
                {
                    TourId = t.Id,
                    TourName = t.Name,
                    TourDifficulty = t.Difficulty,
                    TourDescription = t.Description,
                    Tags = t.Tags,
                    FirstKeyPoint = firstKeyPoint
                };
            }).ToList();

            var pagedResult = new PagedResult<TourOverview>(tours, totalCount);
            return pagedResult;
        }
    }
}
