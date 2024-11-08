using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IAppReviewService, AppReviewService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IProblemService, ProblemService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IClubInvitationService, ClubInvitationService>();
        services.AddScoped<IClubJoinRequestService, ClubJoinRequestService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IProfileMessageService, ProfileMessageService>();
    }


    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<AppReview>), typeof(CrudDatabaseRepository<AppReview, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Problem>), typeof(CrudDatabaseRepository<Problem, StakeholdersContext>));
        services.AddScoped<IProblemRepository, ProblemRepository>();
        services.AddScoped<IClubRepository, ClubDatabaseRepository>();
        services.AddScoped<IClubInvitationRepository, ClubInvitationDatabaseRepository>();
        services.AddScoped<IClubJoinRequestRepository, ClubJoinRequestRepository>();
        services.AddScoped(typeof(ICrudRepository<ClubInvitation>), typeof(CrudDatabaseRepository<ClubInvitation, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ClubJoinRequest>), typeof(CrudDatabaseRepository<ClubJoinRequest, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped<IProfileMessageRepository, ProfileMessageDatabaseRepository>();

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));


    }
}