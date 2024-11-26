using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class SalesService : CrudService<SalesDto, Sales>, ISalesService
    {
        public SalesService(ICrudRepository<Sales> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
        public Result<List<TourDto>> GetDiscountedTours(List<TourDto> allTours)
        {
            
            var activeSales = CrudRepository.GetPaged(1, int.MaxValue) 
                .Results
                .Where(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now)
                .ToList();

            var discountedTours = new List<TourDto>();

            foreach (var sale in activeSales)
            {
                foreach (var tourId in sale.TourIds)
                {
                    var tour = allTours.FirstOrDefault(t => t.Id == tourId);
                    if (tour != null)
                    {
                        var discountedTour = new TourDto(
                           id: tour.Id,
                           name: tour.Name,
                           description: tour.Description,
                           difficulty: tour.Difficulty,
                           tags: tour.Tags,
                           userId: tour.UserId,
                           status: tour.Status,
                           price: tour.Price,
                           discountedPrice: tour.Price * (1 - sale.DiscountPercentage / 100),
                           lengthInKm: tour.LengthInKm,
                           publishedTime: tour.PublishedTime,
                           archivedTime: tour.ArchiveTime,
                           equipmentIds: tour.EquipmentIds,
                           keyPointIds: tour.KeyPoints.Select(k => k.Id).ToList()
                       );

                        discountedTours.Add(discountedTour);
                    }
                }
            }

            return Result.Ok(discountedTours);
        }



        public Result<List<SalesDto>> GetAll(long userId)
        {
            var userSales = CrudRepository.GetPaged(1, int.MaxValue)
                   .Results
                   .Where(s => s.AuthorId == userId)
                   .ToList();

            var salesDtos = userSales.Select(MapToDto).ToList();

            return Result.Ok(salesDtos);
        }
    }

}
