using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Public.TourReviewing;

namespace Explorer.Tours.Core.UseCases.TourReviewing
{
    public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
    {
        public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
