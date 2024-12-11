using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Internal
{
    public interface IAdvertisementTourService
    {
        Result<List<TourDto>> GetAllTours();
        Result<List<TourPreferenceDto>> GetAllToursPreferences();
    }
}
