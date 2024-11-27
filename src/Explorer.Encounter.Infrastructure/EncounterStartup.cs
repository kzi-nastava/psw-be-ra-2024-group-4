using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounter.Core.Domain.RepositoryInterfaces;
using Explorer.Encounter.Core.Mappers;
using Explorer.Encounter.Infrastructure.Database;
using Explorer.Encounter.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounter.API;
using Explorer.Encounter.Core.UseCases;
using Explorer.Encounter.API.Public;

namespace Explorer.Encounter.Infrastructure
{
    public static class EncounterStartup
    {
        public static IServiceCollection ConfigureEncounterModule(this IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(EncounterProfile).Assembly); // Preduslov da imamo ovu liniju koda je da smo definisali već Profile klasu u Core/Mappers
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            // Registracija Core servisa (dodajte konkretne servise prema potrebi)
            services.AddScoped<IEncounterService, EncounterService>();
            services.AddScoped<IImageService, ImageService>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            // Registracija repozitorijuma (CRUD i specifični repozitorijumi)
            services.AddScoped(typeof(ICrudRepository<Core.Domain.Encounter>), typeof(CrudDatabaseRepository<Core.Domain.Encounter, EncounterContext>));

            services.AddScoped<IEncounterRepository, EncounterRepository>();

            // Registracija EncounterContext sa PostgreSQL podešavanjima
            services.AddDbContext<EncounterContext>(opt =>
               opt.UseNpgsql(DbConnectionStringBuilder.Build("encounter"),
                   x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounter")));
        }

    }
}
