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


        public void ApplySaleToTours(SalesDto sale, List<TourDto> allTours)
        {
            foreach (var tour in allTours.Where(t => sale.TourIds.Contains((int)t.Id)))
            {
                tour.DiscountPrice = Math.Round(tour.Price - (tour.Price * sale.DiscountPercentage / 100), 2);
            }
        }

        public Result<List<SalesDto>> GetAllForUser(long userId)
        {
            var userSales = CrudRepository.GetPaged(1, int.MaxValue)
                   .Results
                   .Where(s => s.AuthorId == userId)
                   .ToList();

            var salesDtos = userSales.Select(MapToDto).ToList();

            return Result.Ok(salesDtos);
        }
        public Result<List<SalesDto>> GetAll()
        {
            var allSales = CrudRepository.GetPaged(1, int.MaxValue)
                .Results
                .ToList();

            var salesDtos = allSales.Select(MapToDto).ToList();

            return Result.Ok(salesDtos);
        }

    }

}
