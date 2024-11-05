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

        public Result<List<TourReviewDto>> GetByTouristId(long touristId)
        {
            var reviews = _tourReviewRepository.GetByTouristId(touristId);
            if (reviews.Count == 0)
                return Result.Fail<List<TourReviewDto>>("No found Reviews");
            return MapToDto(reviews);
        }
        public Result<List<TourReviewDto>> GetByTourId(long TourId)
        {
            var reviews = _tourReviewRepository.GetByTourId(TourId);
            if (reviews.Count == 0)
                return Result.Fail<List<TourReviewDto>>("No found Reviews");

            return MapToDto(reviews);
        }
    }
}
