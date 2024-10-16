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

        //public TourPreferenceService(ICrudRepository<TourPreference> repository, IMapper mapper, ITourPreferenceRepository tourPreferenceRepository) : base(repository, mapper) {
        //    _tourPreferenceRepository = tourPreferenceRepository;
        //}
        public TourPreferenceService(ITourPreferenceRepository tourPreferenceRepository) {
            _tourPreferenceRepository = tourPreferenceRepository;
        }
        public Result<TourPreferenceDto> GetTourPreference(int touristId) {
            throw new NotImplementedException();
        }

        public Result UpdateTourPreference(int touristId, TourPreferenceDto preference) {
            throw new NotImplementedException();
        }
    }
}
