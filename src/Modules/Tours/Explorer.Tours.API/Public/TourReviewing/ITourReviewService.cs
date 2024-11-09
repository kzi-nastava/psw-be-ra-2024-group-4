using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourReviewing
{
    public interface ITourReviewService
    {
        Result<PagedResult<TourReviewDto>> GetPaged(int page, int pageSize);
        Result<TourReviewDto> Create(TourReviewDto tourReview);
        Result<TourReviewDto> Update(TourReviewDto tourReview);
        Result Delete(int id);
        Result<PagedResult<TourReviewDto>> GetByTouristId(long id);
        Result<PagedResult<TourReviewDto>> GetByTourId(long id,int page,int pageSize);
    }
}
