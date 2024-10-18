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
            throw new NotImplementedException();
        }

        public Result UpdateTourPreference(int touristId, TourPreferenceDto preference) {
            throw new NotImplementedException();
        }
        public Result AddTourPreference(int touristId, TourPreferenceDto preference) {
            newPreference.TouristId = touristId;
            var newPreference = _mapper.Map<TourPreference>(preference);
            _tourPreferenceRepository.Add(newPreference);
            return Result.Ok();
        }
    }
}
