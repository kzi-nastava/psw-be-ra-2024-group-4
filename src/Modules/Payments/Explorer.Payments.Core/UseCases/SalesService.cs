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
        public Result<List<TourOverviewDto>> GetDiscountedTours(List<TourOverviewDto> allTours)
        {
            // Dobavljanje svih aktivnih popusta
            var activeSales = CrudRepository.GetPaged(1, int.MaxValue)
                .Results
                .Where(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now)
                .ToList();

            var discountedTours = new List<TourOverviewDto>();

            // Iteracija kroz sve aktivne akcije
            foreach (var sale in activeSales)
            {
                // Iteracija kroz sve ture u akciji
                foreach (var tourId in sale.TourIds)
                {
                    var tour = allTours.FirstOrDefault(t => t.TourId == tourId);

                    // Ako je tura pronađena, primeni popust
                    if (tour != null)
                    {
                        // Proračun snižene cene
                        var discountedPrice =(tour.Price * (1 - (decimal)sale.DiscountPercentage / 100));

                        // Kreiranje novog DTO objekta sa popustom
                        var discountedTour = new TourOverviewDto()
                        {
                            TourId = tour.TourId,
                            TourName = tour.TourName,
                            TourDescription = tour.TourDescription,
                            TourDifficulty = tour.TourDifficulty,
                            Tags = tour.Tags,
                            Price = discountedPrice,  // Snižena cena
                            OriginalPrice = tour.Price, // Originalna cena
                            DiscountPercentage = (decimal)sale.DiscountPercentage, // Popust u procentima
                            FirstKeyPoint = tour.FirstKeyPoint,  // Prvi ključni trenutak
                            Reviews = tour.Reviews,  // Recenzije
                            AverageRating = tour.AverageRating  // Prosečna ocena
                        };

                        discountedTours.Add(discountedTour);  // Dodaj turu sa popustom u listu
                    }
                }
            }

            return Result.Ok(discountedTours);  // Vratite sve ture sa popustom
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
