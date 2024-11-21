using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using Explorer.Encounter.Core.Domain.RepositoryInterfaces;
using Explorer.Encounter.API.Public;

namespace Explorer.Encounter.Core.UseCases
{
    public class EncounterService : CrudService<EncounterDto, Domain.Encounter>, IEncounterService
    {
        private IEncounterRepository _encounterRepository;
        public EncounterService(ICrudRepository<Domain.Encounter> crudRepository, IMapper mapper, IEncounterRepository encounterRepository) : base(crudRepository, mapper)
        {
            _encounterRepository = encounterRepository;
        }
    }
}
