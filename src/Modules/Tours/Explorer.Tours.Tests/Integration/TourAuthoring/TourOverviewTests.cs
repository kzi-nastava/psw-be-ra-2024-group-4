using AutoMapper;
using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.UseCases.TourAuthoring;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Validations;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourAuthoring
{
    [Collection("Sequential")]
    public class TourOverviewTests : BaseToursIntegrationTest
    {
        public TourOverviewTests(ToursTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Get_all_without_reviews()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var result = ((ObjectResult)controller.GetAllWithoutreviews(0, 0).Result)?.Value as PagedResult<TourOverviewDto>;

            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(0);
            result.TotalCount.ShouldBe(2);
        }

        [Fact]
        public void Get_all_by_tour_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var result = ((ObjectResult)controller.GetAllByTourId(-1, 0, 0).Result)?.Value as PagedResult<TourOverviewDto>;

            result.ShouldBeNull(); // treba promeniti testnu baze da se TourId u tourreview poklapa sa id tura koje postoje
        }

        private static TourOverviewController CreateController(IServiceScope scope)
        {
            return new TourOverviewController(scope.ServiceProvider.GetRequiredService<ITourOverviewService>());
        }
    }
}
