using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using Explorer.Encounter.Core.Domain.RepositoryInterfaces;
using Explorer.Encounter.API.Public;
using FluentResults;
using System.Collections;

namespace Explorer.Encounter.Core.UseCases
{
    public class EncounterService : CrudService<EncounterDto, Domain.Encounter>, IEncounterService
    {
        private ICrudRepository<Domain.Encounter> _encounterRepository;
        public EncounterService(ICrudRepository<Domain.Encounter> crudRepository, IMapper mapper, 
            IEncounterRepository encounterRepository) : base(crudRepository, mapper)
        {
            _encounterRepository = crudRepository;
        }

        public Result<EncounterDto> CreateEncounter(EncounterDto encounterDto)
        {
            try
            {
                var ret = _encounterRepository.Create(MapToDomain(encounterDto));
                return Result.Ok(MapToDto(ret));
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<PagedResult<EncounterDto>> Get(int page, int pageSize)
        {
            try
            {
                var ret = _encounterRepository.GetPaged(page, pageSize);
                List<EncounterDto> encounterList = new List<EncounterDto>();
                foreach (var item in ret.Results) 
                {
                    encounterList.Add(MapToDto(item));
                }
                return Result.Ok(new PagedResult<EncounterDto>(encounterList, encounterList.Count()));
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<PagedResult<EncounterDto>> GetInRadius(double radius, double lat, double lon)
        {
            const double EarthRadiusKm = 6371.0;

            double CalculateSquaredDistance(double lat1, double lon1, double lat2, double lon2)
            {
                double dLat = lat2 - lat1;
                double dLon = lon2 - lon1;

                return (dLat * dLat) + (dLon * dLon);
            }

            var encounters = _encounterRepository.GetPaged(0, 0).Results;

            var filteredEncounters = encounters.FindAll(encounter =>
            {
                double squaredDistance = CalculateSquaredDistance(lat, lon, encounter.Latitude, encounter.Longitude);

                return squaredDistance <= (radius * radius / (EarthRadiusKm * EarthRadiusKm));
            });

            List<EncounterDto> ret = new List<EncounterDto>();
            foreach (var item in filteredEncounters)
            {
                ret.Add(MapToDto(item));
            }

            return Result.Ok(new PagedResult<EncounterDto>(ret, filteredEncounters.Count()));
        }

    }
}
