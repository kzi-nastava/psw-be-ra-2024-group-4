using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        public PagedResult<TourReview> GetByTourId(long tourId, int page, int pageSize);
        public TourReview Get(long userId, long tourId);
    }
}
