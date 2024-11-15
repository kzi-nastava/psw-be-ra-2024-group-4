using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Geolocation;
using System.Security.Cryptography.X509Certificates;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class TourOverviewService : ITourOverviewService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly ITourReviewRepository _tourReviewRepository;
        IMapper _mapper { get; set; }

        public TourOverviewService(ITourRepository tourRepository, IKeyPointRepository keyPointRepository,
            ITourReviewRepository tourReviewRepository, IMapper mapper)
        {
            this._tourRepository = tourRepository;
            this._keyPointRepository = keyPointRepository;
            this._tourReviewRepository = tourReviewRepository;
            this._mapper = mapper;
        }

        public Result<PagedResult<TourReviewDto>> GetAllByTourId(int page, int pageSize, long tourId)
        {
            var result = _tourReviewRepository.GetByTourId(tourId, page, pageSize);
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
            var publishedTours = _tourRepository.GetPublished(page, pageSize);

            var ret = publishedTours.ToResult();

            if (ret.IsFailed || ret.Value.Results.Any(x => x.KeyPoints.Count() == 0))
            {
                return Result.Fail(ret.Errors);
            }

            var pagedItems = new List<TourOverviewDto>();
            foreach (var tour in ret.Value.Results)
            {
                var tags = tour.Tags.Select(t => t.ToString()).ToList();

                var newTourOverview = new TourOverviewDto()
                {
                    TourId = tour.Id,
                    TourDescription = tour.Description,
                    Tags = tags,
                    TourDifficulty = tour.Difficulty,
                    TourName = tour.Name,
                    Price = Convert.ToDecimal(tour.Price),
                    FirstKeyPoint = _mapper.Map<KeyPointDto>(tour.KeyPoints.First()),
                    Reviews = new List<TourReviewDto>()
                };
                
                pagedItems.Add(newTourOverview);
            }

            var pagedResult = new PagedResult<TourOverviewDto>(pagedItems, pagedItems.Count());

            return Result.Ok(pagedResult);
        }

        public Result<TourOverviewDto> GetAverageRating(long tourId)
        {
            var result = _tourReviewRepository.GetByTourId(tourId, 0, 0);
            var ret = result.ToResult();

            if(ret.Errors == null)
            {
                return Result.Fail(ret.Errors);
            }

            var averageRating = 0;
            foreach (var r in ret.Value.Results)
            {
                averageRating += r.Rating;
            }

            var totalCount = ret.Value.Results.Count();
            averageRating = totalCount <= 0 ? averageRating : averageRating/totalCount;

            return Result.Ok(new TourOverviewDto { AverageRating = averageRating });
        }

        public Result<PagedResult<TourOverviewDto>> GetByCoordinated(double latitude, double longitude, int distance, int page, int pageSize)
        {
            var res = new List<KeyPoint>();
            var centralCoordinate = new Coordinate(latitude, longitude);
            var keyPoints = _keyPointRepository.GetAll();


            foreach (var kp in keyPoints)
            {
                double dist = GeoCalculator.GetDistance(
                centralCoordinate,
                new Coordinate(kp.Latitude, kp.Longitude),
                4,
                DistanceUnit.Kilometers);
                if (dist < distance)
                    res.Add(kp);
            }

            var tours = _tourRepository.GetByKeyPoints(res, 0, 0);
            var ret = tours.ToResult();

            var pagedItems = new List<TourOverviewDto>();
            foreach (var tour in ret.Value.Results)
            {
                var tags = tour.Tags.Select(t => t.ToString()).ToList();

                var newTourOverview = new TourOverviewDto()
                {
                    TourId = tour.Id,
                    TourDescription = tour.Description,
                    Tags = tags,
                    TourDifficulty = tour.Difficulty,
                    TourName = tour.Name,
                    Price = Convert.ToDecimal(tour.Price),
                    FirstKeyPoint = _mapper.Map<KeyPointDto>(tour.KeyPoints.First()),
                    Reviews = new List<TourReviewDto>()
                };

                pagedItems.Add(newTourOverview);
            }

            var pagedResult = new PagedResult<TourOverviewDto>(pagedItems, pagedItems.Count());

            return Result.Ok(pagedResult);
        }
    }
}
