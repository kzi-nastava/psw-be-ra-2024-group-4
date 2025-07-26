using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Explorer.Payments.Infrastructure
{
    public static class PaymentsStartup
    {

        public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PaymentsProfile).Assembly); 
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IShoppingCartService, ShoppingService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();
            services.AddScoped<IChatbotService, ChatbotService>();
            services.AddScoped<IBundleService, BundleService>();
            services.AddScoped<IPaymentRecordService, PaymentRecordService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<ISalesService,SalesService>();

        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<OrderItem>), typeof(CrudDatabaseRepository<OrderItem, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<TourPurchaseToken>), typeof(CrudDatabaseRepository<TourPurchaseToken, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<Bundle>), typeof(CrudDatabaseRepository<Bundle, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<PaymentRecord>), typeof(CrudDatabaseRepository<PaymentRecord, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<Coupon>),typeof(CrudDatabaseRepository<Coupon, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<Sales>),typeof(CrudDatabaseRepository<Sales, PaymentsContext>));

            services.AddScoped<IShoppingCartRepository, ShoppingRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<ITourPurchaseTokenRepository, TourPurchaseTokenRepository>();
            services.AddScoped<IBundleRepository, BundleRepository>();
            services.AddScoped<IPaymentRecordRepository, PaymentRecordRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();



            services.AddDbContext<PaymentsContext>(opt =>
           opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
               x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));

        }
    }
}
