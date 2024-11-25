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
        private readonly IEncounterRepository _customEncounterRepository;
        private readonly IMapper _mapper;
        public EncounterService(ICrudRepository<Domain.Encounter> crudRepository, IMapper mapper, 
            IEncounterRepository encounterRepository) : base(crudRepository, mapper)
        {
            _encounterRepository = crudRepository;
            _customEncounterRepository = encounterRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
            {
                double dLat = Math.PI / 180 * (lat2 - lat1);
                double dLon = Math.PI / 180 * (lon2 - lon1);

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                           Math.Cos(Math.PI / 180 * lat1) * Math.Cos(Math.PI / 180 * lat2) *
                           Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return EarthRadiusKm * c; 
            }

            var encounters = _encounterRepository.GetPaged(0, 0).Results;

            var filteredEncounters = encounters.FindAll(encounter =>
            {
                double distance = CalculateDistance(lat, lon, encounter.Latitude, encounter.Longitude);

                Console.WriteLine($"Encounter {encounter.Title}: Udaljenost = {distance} km");
                return distance <= radius;
            });

            Console.WriteLine($"Ukupno filtriranih susreta: {filteredEncounters.Count}");

            List<EncounterDto> ret = new List<EncounterDto>();
            foreach (var item in filteredEncounters)
            {
                ret.Add(MapToDto(item));
            }

            return Result.Ok(new PagedResult<EncounterDto>(ret, filteredEncounters.Count()));
        }

        public Result<EncounterDto> ActivateEncounter(long userId, long encounterId, double longitude, double latitude)
        {
            try
            {
                var encounter = _customEncounterRepository.GetById(encounterId);
                encounter.ActivateEncounter(userId, longitude, latitude);
                CrudRepository.Update(encounter);
                return MapToDto(encounter);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<EncounterDto> CompleteEncounter(long userId, long encounterId)
        {
            try
            {
                var encounter = _customEncounterRepository.GetById(encounterId);
                encounter.CompleteEncounter(userId);
                _encounterRepository.Update(encounter);
                var responseDto = _mapper.Map<EncounterDto>(encounter);

                return responseDto;
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }


        public Result<EncounterDto> GetByLatLong(double lat, double lon)
        {
            var encounter = _encounterRepository.GetPaged(0, 0).Results.Find(encounter => { return encounter.Latitude == lat && encounter.Longitude == lon; });
            EncounterDto ret = MapToDto(encounter);

            
            return Result.Ok(ret);
        }
    }
}
