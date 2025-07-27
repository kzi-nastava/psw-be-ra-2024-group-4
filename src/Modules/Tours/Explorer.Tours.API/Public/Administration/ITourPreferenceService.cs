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
        Task<List<TourPreferenceDto>> GetAllPreferencesAsync();
        Result UpdateTourPreference(int touristId, TourPreferenceDto preference);
        Result AddTourPreference(int touristId, TourPreferenceDto preference);
        Result<bool> HasTourPreference(int touristId);
    }
}
