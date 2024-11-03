using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class TourOverviewService : ITourOverviewService
    {
        private readonly ITourOverviewRepository tourOverviewRepository;
        IMapper _mapper { get; set; }

        public TourOverviewService(ITourOverviewRepository tourOverviewRepository, IMapper mapper)
        {
            this.tourOverviewRepository = tourOverviewRepository;
            this._mapper = mapper;
        }

        public Result<PagedResult<TourReviewDto>> GetAllByTourId(int page, int pageSize, long tourId)
        {
            var result = tourOverviewRepository.GetAllByTourId(page, pageSize, tourId);
            var ret = result.ToResult();

            if (ret.IsFailed)
            {
                return Result.Fail(ret.Errors);
            }

            var items = ret.Value.Results.Select(_mapper.Map<TourReviewDto>).ToList();
            var pagedResult = new PagedResult<TourReviewDto>(items, ret.Value.TotalCount);

            return Result.Ok(pagedResult);
        }

        public Result<PagedResult<TourOverviewDto>> GetAllWithoutReviews(int page, int pageSize)
        {
            var result = tourOverviewRepository.GetAllWithoutReviews(page, pageSize);
            var ret = result.ToResult();

            if (ret.IsFailed)
            {
                return Result.Fail(ret.Errors);
            }

            var items = ret.Value.Results.Select(_mapper.Map<TourOverviewDto>).ToList();
            var pagedResult = new PagedResult<TourOverviewDto>(items, ret.Value.TotalCount);

            return Result.Ok(pagedResult);
        }
    }
}
