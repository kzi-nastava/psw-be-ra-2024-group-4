using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Explorer.Tours.Core.UseCases.Administration {
    public class TourPreferenceService : ITourPreferenceService {

        private readonly ITourPreferenceRepository _tourPreferenceRepository;
        private readonly IMapper _mapper;

        //public TourPreferenceService(ICrudRepository<TourPreference> repository, IMapper mapper, ITourPreferenceRepository tourPreferenceRepository) : base(repository, mapper) {
        //    _tourPreferenceRepository = tourPreferenceRepository;
        //}
        public TourPreferenceService(ITourPreferenceRepository tourPreferenceRepository,IMapper mapper) {
            _tourPreferenceRepository = tourPreferenceRepository;
            _mapper = mapper;
        }
        public async Task<List<TourPreferenceDto>> GetAllPreferencesAsync() {
            var preferences = await _tourPreferenceRepository.GetAll();
            return _mapper.Map<List<TourPreferenceDto>>(preferences);
        }
        public Result<TourPreferenceDto> GetTourPreference(int touristId) {
            var preference = _tourPreferenceRepository.GetByTouristId(touristId).FirstOrDefault();
            return _mapper.Map<TourPreferenceDto>(preference);
        }

        public Result UpdateTourPreference(int touristId, TourPreferenceDto preference) {
            var existingPreferenceResult = _tourPreferenceRepository.GetByTouristId(touristId);
            if (existingPreferenceResult == null || !existingPreferenceResult.Any()) {
                return Result.Fail("Tour preference for wanted tourist not found!");
            }
            var existingPreference = existingPreferenceResult.FirstOrDefault();
            _mapper.Map(preference, existingPreference);
            _tourPreferenceRepository.Update(existingPreference);

            return Result.Ok();
        }
        public Result AddTourPreference(int touristId, TourPreferenceDto preference) {
            preference.TouristId = touristId;
            var newPreference = _mapper.Map<TourPreference>(preference);
            _tourPreferenceRepository.Add(newPreference);
            return Result.Ok();
        }
    }
}
