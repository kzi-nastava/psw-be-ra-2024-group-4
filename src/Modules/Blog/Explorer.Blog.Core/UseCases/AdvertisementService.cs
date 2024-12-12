using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using FluentResults;

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

        

        public Result<AdvertisementDto> OperateAll()
        {
            
            List<AdvertisementDto> advertisements = ProlongValidity(GetAll());

            //Gather the rest of the necessary data
            var tourists = _advertisementStakeholdersService.GetAllTourists().Value;
            var touristPreferences = _advertisementTourService.GetAllToursPreferences().Value;
            var tours = _advertisementTourService.GetAllTours().Value;   
            var clubs = _advertisementStakeholdersService.GetAllClubs().Value;


            DateTime lastTourDate = FindLastTwoValidTo(advertisements).Item1;
            DateTime lastClubDate = FindLastTwoValidTo(advertisements).Item2;

            foreach (var tourist in tourists)
            {
                //If tourist has tourPreferences make personalised ads
                if(touristPreferences.Any(tp => tp.TouristId == tourist.Id) != null)
                {

                }
                //If tourist does not have tourPreference make ads for all tours and clubs
                else
                {
                    
                }
            }

            return null;
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

        //This method takes all the advertisements from the db and prolongs their validity
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

        private List<AdvertisementDto> CreateForTours(List<TourDto> tours)
        {
            return null;
        }

        private List<AdvertisementDto> CreateForClubs(List<ClubDto> clubs)
        {
            return null;
        }
    }
}
