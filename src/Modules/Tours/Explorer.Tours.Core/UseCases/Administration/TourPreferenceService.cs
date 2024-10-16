using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration {
    public class TourPreferenceService : CrudService<TourPreferenceDto, TourPreference>, ITourPreferenceService {

        public TourPreferenceService(ICrudRepository<TourPreference> repository, IMapper mapper) : base(repository, mapper) { }
        public Result<TourPreferenceDto> GetTourPreferenceAsync(int touristId) {
            throw new NotImplementedException();
        }

        public Result UpdateTourPreferenceAsync(int touristId, TourPreferenceDto preference) {
            throw new NotImplementedException();
        }
    }
}
