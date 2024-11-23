using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Public.TourReviewing;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System.Dynamic;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourReviewing
{
    public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
    {

        private readonly ITourReviewRepository _tourReviewRepository;
        public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper, ITourReviewRepository tourReviewRepository) : base(repository, mapper)
        {
            _tourReviewRepository = tourReviewRepository;
        }

        public Result<TourReviewDto>Get(long userId, long tourId) 
        {

            var tourReview = _tourReviewRepository.Get(userId, tourId);


            if (tourReview == null)
            {
                
                return Result.Ok<TourReviewDto>(null); 
            }

            var tourReviewDto = new TourReviewDto()
            {
                Id = tourReview.Id,
                IdTour = tourReview.IdTour,
                IdTourist = tourReview.IdTourist,
                Comment = tourReview.Comment,
                Rating = tourReview.Rating,
                DateTour = tourReview.DateTour,
                DateComment = tourReview.DateComment,
                Image = tourReview.Image,
                PercentageCompleted = tourReview.PercentageCompleted,


            };
           return Result.Ok(tourReviewDto);
        }



    }
}
