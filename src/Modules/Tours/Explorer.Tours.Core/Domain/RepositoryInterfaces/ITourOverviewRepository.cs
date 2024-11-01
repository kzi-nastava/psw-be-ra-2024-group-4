using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourOverviewRepository
    {
        PagedResult<TourOverview> GetAllWithoutReviews(int page, int pageSize);
        PagedResult<TourReview> GetAllByTourId(int page, int pageSize, long tourId);
    }
}
