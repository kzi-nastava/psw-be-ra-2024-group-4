using Explorer.BuildingBlocks.Tests;
using Explorer.Encounter.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Explorer.Encounters.Tests;

public class EncountersTestFactory : BaseTestFactory<EncounterContext>
{
    protected override IServiceCollection ReplaceNeededDbContexts(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EncounterContext>));
        services.Remove(descriptor!);
        services.AddDbContext<EncounterContext>(SetupTestContext());
        return services;
    }
}
