using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    internal class AdvertisementTourService : IAdvertisementTourService
    {
        private readonly ITourService _tourService;
        private readonly ITourPreferenceService _tourPreferenceService;
        
        public AdvertisementTourService(ITourService tourService, ITourPreferenceService tourPreferenceService) 
        {
            _tourService = tourService;
            _tourPreferenceService = tourPreferenceService;
        }

        public Result<List<TourDto>> GetAllTours()
        {
            return _tourService.GetAllPublised().ToResult();
        }

        public Result<List<TourPreferenceDto>> GetAllToursPreferences()
        {
            return _tourPreferenceService.GetAllPreferencesAsync().Result;
        }
    }
}
