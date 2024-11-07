using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Public.TourReviewing;
using FluentResults;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases.TourReviewing
{
    public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
    {
        public ITourReviewRepository _tourReviewRepository { get; set; }
        public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper, ITourReviewRepository tourReviewRepository) : base(repository, mapper) 
        {
            _tourReviewRepository = tourReviewRepository;
        }

        public Result<PagedResult<TourReviewDto>> GetByTouristId(long touristId)
        {
            var reviews = _tourReviewRepository.GetByTouristId(touristId);
            return MapToDto(reviews);
        }
        public Result<PagedResult<TourReviewDto>> GetByTourId(long TourId)
        {
            var reviews = _tourReviewRepository.GetByTourId(TourId);
            return MapToDto(reviews);
        }

        public Result<TourReviewDto> GetByTourAndTourist(long tourId, long touristId)
        {
            var review = _tourReviewRepository.GetByTourAndTourist(tourId, touristId);
            return MapToDto(review);
        }
    }
}
