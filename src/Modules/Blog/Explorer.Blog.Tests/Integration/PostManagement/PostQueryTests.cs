using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.PostManagement;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration.PostManagement
{
    [Collection("Sequential")]
    public class PostQueryTests: BaseBlogIntegrationTest
    {
        public PostQueryTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<PostDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }


        private static PostController CreateController(IServiceScope scope)
        {
            return new PostController(scope.ServiceProvider.GetRequiredService<IPostService>(), scope.ServiceProvider.GetRequiredService<ICommentService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())

            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
