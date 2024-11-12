using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Explorer.Tours.API.Public.TourReviewing;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.TourReviewing;
using Explorer.Tours.Core.UseCases.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.UseCases.TourAuthoring.ObjectAddition;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.UseCases.TourAuthoring;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.TourShopping;
using Explorer.Tours.API.Public.TourShopping;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution;
using Explorer.Tours.Core.UseCases.Execution;
using Explorer.Tours.Infrastructure.Database.Repositories.Execution;

using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<ITourPreferenceService, TourPreferenceService>();
        services.AddScoped<IKeyPointService, KeyPointService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IObjectService, ObjectService>();
        services.AddScoped<ITourReviewService, TourReviewService>();
        services.AddScoped<IShoppingCartService, ShoppingService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
        services.AddScoped<IPositionSimulatorService, PositionSimulatorService>();
        services.AddScoped<ITourOverviewService, TourOverviewService>();
        services.AddScoped<IShoppingCartService, ShoppingService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
        services.AddScoped<IPositionSimulatorService, PositionSimulatorService>();
        services.AddScoped<ITourOverviewService, TourOverviewService>();

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        services.AddScoped<ITourPreferenceRepository,TourPreferenceRepository>();
        services.AddScoped(typeof(ICrudRepository<KeyPoint>), typeof(CrudDatabaseRepository<KeyPoint, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<OrderItem>), typeof(CrudDatabaseRepository<OrderItem, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<PositionSimulator>), typeof(CrudDatabaseRepository<PositionSimulator, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Core.Domain.Object>), typeof(CrudDatabaseRepository<Core.Domain.Object, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourPurchaseToken>), typeof(CrudDatabaseRepository<TourPurchaseToken, ToursContext>));
        services.AddScoped<ITourRepository, TourRepository>();
        services.AddScoped<IKeyPointRepository, KeyPointRepository>();
        services.AddScoped<IObjectRepository, ObjectRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<ITourPurchaseTokenRepository, TourPurchaseTokenRepository>();
        services.AddScoped<ITourExecutionRepository, TourExecutionRepository>();
        services.AddScoped<ITourReviewRepository, TourReviewRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<ITourPurchaseTokenRepository, TourPurchaseTokenRepository>();
        services.AddScoped<ITourExecutionRepository, TourExecutionRepository>();
        services.AddScoped<ITourReviewRepository, TourReviewRepository>();
        services.AddScoped(typeof(ICrudRepository<TourReview>), typeof(CrudDatabaseRepository<TourReview, ToursContext>));
        services.AddScoped<IPositionSimulatorRepository, PositionSimulatorRepository>();
        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}