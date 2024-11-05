using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourAuthoring
{
    public interface ITourOverviewService
    {
        Result<PagedResult<TourOverviewDto>> GetAllWithoutReviews(int page, int pageSize);
        Result<PagedResult<TourReviewDto>> GetAllByTourId(int page, int pageSize, long tourId);
        Result<TourOverviewDto> GetAverageRating(long tourId);
    }
}
