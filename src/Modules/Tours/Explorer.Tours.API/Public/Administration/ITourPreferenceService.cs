using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration {
    public interface ITourPreferenceService {

        Result <TourPreferenceDto> GetTourPreference(int touristId);
        Result UpdateTourPreference(int touristId, TourPreferenceDto preference);
    }
}
