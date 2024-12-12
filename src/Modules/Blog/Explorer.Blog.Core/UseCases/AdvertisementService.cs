using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using static Explorer.Stakeholders.API.Dtos.ClubDto;

namespace Explorer.Blog.Core.UseCases
{
    public class AdvertisementService : CrudService<AdvertisementDto, Advertisement>, IAdvertisementService
    {
        private readonly ICrudRepository<Advertisement> _crudRepository;
        private readonly IAdvertisementStakeholdersService _advertisementStakeholdersService;
        private readonly IAdvertisementTourService _advertisementTourService;
        public AdvertisementService(ICrudRepository<Advertisement> crudRepository, IMapper mapper, IAdvertisementStakeholdersService advertisementStakeholdersService, IAdvertisementTourService advertisementTourService) : base(crudRepository, mapper)
        {
            _crudRepository = crudRepository;
            _advertisementStakeholdersService = advertisementStakeholdersService;
            _advertisementTourService = advertisementTourService;
        }

        public Result<List<AdvertisementDto>> GetByTourist(int touristId)
        {
            var advertisements = _crudRepository.GetPaged(0, 0).Results;

            var matchingAdvertisements = advertisements.FindAll(advertisement => advertisement.TouristId == touristId).ToList();
            
            List<AdvertisementDto> result = new List<AdvertisementDto>();
            foreach(var  advertisement in matchingAdvertisements)
            {
                result.Add(MapToDto(advertisement));
            }
            
            if (result.Count > 0)
            {
                return Result.Ok(result);
            }
            else
            {
                return Result.Fail("No advertisements found for touristId: " + touristId);
            }

        }

        public void Operate()
        {
            List<AdvertisementDto> advertisements = ProlongValidity(GetAll());
            
            //Gather the rest of the necessary data
            var tourists = _advertisementStakeholdersService.GetAllTourists().Value;
            var touristPreferences = _advertisementTourService.GetAllToursPreferences().Value;
            var tours = _advertisementTourService.GetAllTours().Value;   
            var clubs = _advertisementStakeholdersService.GetAllClubs().Value;
            
            //Get last valid date for tourAd and clubAd
            DateTime lastTourDate = FindLastTwoValidTo(advertisements).Item1;
            DateTime lastClubDate = FindLastTwoValidTo(advertisements).Item2;

            foreach (var tourist in tourists)
            {

                var tourPreference = touristPreferences.Find(tp => tp.TouristId == tourist.Id);
                //If tourist has tourPreferences.Tags make personalised ads
                if (tourPreference != null)
                {
                    var userTags = tourPreference.Tags;
                    var convertedTourTags = userTags.Select(tag => (Tours.API.Dtos.TourTags)tag).ToList();
                    var convertedClubTags = userTags.Select(tag => (ClubTags)tag).ToList();

                    lastTourDate = CreateTourAd(tours.FindAll(t => t.Tags.Any(tag => convertedTourTags.Contains(tag))), tourist.Id, lastTourDate);
                    lastClubDate = CreateClubAd(clubs.FindAll(t => t.Tags.Any(tag => convertedClubTags.Contains(tag))), tourist.Id, lastClubDate);
                }
                //If tourist does not have tourPreference.Tags make ads for all tours and clubs
                else
                {
                    lastTourDate = CreateTourAd(tours, tourist.Id, lastTourDate);
                    lastClubDate = CreateClubAd(clubs, tourist.Id, lastClubDate);
                }
            }
        }

        private List<AdvertisementDto> GetAll()
        {
            var advertisements = _crudRepository.GetPaged(0, 0).Results;

            List<AdvertisementDto> mapped = new List<AdvertisementDto>();
            foreach( var advertisement in advertisements)
            {
                mapped.Add(MapToDto(advertisement));
            }


            return mapped;
        }

        private List<AdvertisementDto> ProlongValidity(List<AdvertisementDto> advertisements)
        {
            if (advertisements.Count == 0) return null;

            DateTime tourLastDate = FindLastTwoValidTo(advertisements).Item1;
            DateTime clubLastDate = FindLastTwoValidTo(advertisements).Item2;

            List<AdvertisementDto> prolongedAds = new List<AdvertisementDto>();
            foreach(AdvertisementDto advertisement in advertisements)
            {
                if(advertisement.ValidTo <= DateTime.Today.AddHours(12))
                {
                    if(advertisement.TourId != 0)
                    {
                        advertisement.ValidTo = tourLastDate.AddDays(1);
                        tourLastDate = advertisement.ValidTo;
                    }
                    else
                    {
                        advertisement.ValidTo = clubLastDate.AddDays(1);
                        clubLastDate = advertisement.ValidTo;
                    }


                    Update(advertisement);
                }

                prolongedAds.Add(advertisement);
            }

            return prolongedAds;
        }
        
        private (DateTime, DateTime) FindLastTwoValidTo(List<AdvertisementDto> advertisements)
        {
            if(advertisements == null)
            {
                DateTime newStartDate = DateTime.Today.AddHours(12);
                return (newStartDate, newStartDate);
            }

            List<AdvertisementDto> tourAdvertisements = advertisements.FindAll(a => a.TourId != 0);
            List<AdvertisementDto> clubAdvertisements = advertisements.FindAll(a => a.ClubId != 0);

            List<DateTime> tourAdValidities = new List<DateTime>();
            foreach (AdvertisementDto advertisement in tourAdvertisements)
            {
                tourAdValidities.Add(advertisement.ValidTo);
            }

            List<DateTime> clubAdValidities = new List<DateTime>();
            foreach (AdvertisementDto advertisement in clubAdvertisements)
            {
                clubAdValidities.Add(advertisement.ValidTo);
            }

            return (tourAdValidities.Max(), clubAdValidities.Max());
        }

        private AdvertisementDto Update(AdvertisementDto advertisement)
        {
            _crudRepository.Update(MapToDomain(advertisement));

            return advertisement;
        }

        private AdvertisementDto Create(AdvertisementDto advertisement)
        {
            advertisement.Id = _crudRepository.Create(MapToDomain(advertisement)).Id;

            return advertisement;
        }

        private DateTime CreateTourAd(List<TourDto> tours, long touristId, DateTime lastValidDate)
        {
           
            foreach (var tour in tours)
            {
                lastValidDate = lastValidDate.AddDays(1);
                AdvertisementDto newAd = new AdvertisementDto(touristId, tour.Id, 0, lastValidDate);
                Create(newAd);
            }

            return lastValidDate;
        }

        private DateTime CreateClubAd(List<ClubDto> clubs, long touristId, DateTime lastValidDate)
        {
            foreach (var club in clubs)
            {
                lastValidDate = lastValidDate.AddDays(1);
                AdvertisementDto newAd = new AdvertisementDto(touristId, 0, club.Id, lastValidDate);
                Create(newAd);
            }

            return lastValidDate;
        }
    }
}
