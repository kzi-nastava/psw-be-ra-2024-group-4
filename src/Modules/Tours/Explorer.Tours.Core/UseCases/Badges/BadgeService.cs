using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Badges;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Badges
{

    public class BadgeService : CrudService<BadgeDto, Badge>, IBadgeService
    {
        
        IBadgeRepository _badgeRepository {  get; set; }
        ITourRepository _tourRepository { get; set; }

        ITourExecutionRepository _tourExecutionRepository { get; set; }

        IMapper _mapper { get; set; }
        public BadgeService(ICrudRepository<Badge> repository, IMapper mapper, IBadgeRepository badgeRepository, ITourRepository tourRepository, ITourExecutionRepository tourExecutionRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _badgeRepository = badgeRepository;
            _tourRepository = tourRepository;
            _tourExecutionRepository = tourExecutionRepository;
        }

        public Result AddBadgeIfNeeded(long tourId, long userId)
        {
            try
            {
                //deo za tagove
                Tour tour = _tourRepository.GetById(tourId); //dobavi koja tura je zavrsena
                if (tour == null) {
                    return null;
                }
                int numForTag = 0;
                foreach (var tag in tour.Tags) //za svaki tag iz ture
                {
                    numForTag = 0;
                    List<long> tourIdsForTag = _tourRepository.GetIdsByTag(tag);
                    foreach (var id in tourIdsForTag)
                    {
                        bool isCompleted = _tourExecutionRepository.CheckIfCompleted(userId, tourId);
                        if (isCompleted)
                        {
                            numForTag++;
                        }
                    }

                    if (numForTag > 1) {
                        checkForCompletedTag(tag, numForTag, userId);
                        
                        
                    }
                    

                    

                    
                }
                //deo za lengthInKm
                checkSumOfLenth(userId);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        

        public void checkForCompletedTag(Domain.Tours.TourTags tag, int numForTag, long userId)
        {
            Domain.Badge.AchievementLevels level = Badge.AchievementLevels.None;
            if(numForTag >= 7)
            {
                level = Badge.AchievementLevels.Gold;
            }
            else if(numForTag >= 4)
            {
                level = Badge.AchievementLevels.Silver;
            }
            else
            {
                level = Badge.AchievementLevels.Bronze;
            }

            if (!_tagToBadgeMapping.TryGetValue(tag, out var badgeName))
            {
                badgeName = Domain.Badge.BadgeName.CulturalEnthusiast;
            }

            _badgeRepository.AddBadgeIfNotExist(badgeName, level, userId);

        }


        private void checkSumOfLenth(long userId)
        {
            List<long> completedTourIds = _tourExecutionRepository.FindAllCompletedForUser(userId);
            double sumOfLenght = _tourRepository.SumOfTourLenght(completedTourIds);
            double maxLength = _tourRepository.FindMaxTourLength(completedTourIds);

            if (sumOfLenght > 1000)
            {
                _badgeRepository.AddBadgeIfNotExist(Domain.Badge.BadgeName.Globetrotter, Badge.AchievementLevels.None, userId);
            }
            else if (sumOfLenght > 100)
            {
                _badgeRepository.AddBadgeIfNotExist(Domain.Badge.BadgeName.ExplorerStep, Badge.AchievementLevels.None, userId);
            }

            if (maxLength > 400) {
                _badgeRepository.AddBadgeIfNotExist(Domain.Badge.BadgeName.TourTaster, Badge.AchievementLevels.None, userId);
            }

        }

        public Result<List<BadgeDto>> getAll()
        {
            var badges = _badgeRepository.getAll();
            var badgeDtos = badges.Select(b => new BadgeDto
            {
                Id = b.Id,
                Level = (API.Dtos.BadgeDto.AchievementLevels)b.Level,
                Name = (API.Dtos.BadgeDto.BadgeName)b.Name,
                UserId = b.UserId,
            }).ToList();

            return Result.Ok(badgeDtos);
        }

        public Result<List<BadgeDto>> getAllNotRead()
        {
            var badges = _badgeRepository.getAllNotRead();
            var badgeDtos = badges.Select(b => new BadgeDto
            {
                Id = b.Id,
                Level = (API.Dtos.BadgeDto.AchievementLevels)b.Level,
                Name = (API.Dtos.BadgeDto.BadgeName)b.Name,
                UserId = b.UserId,
                IsRead = b.IsRead,
            }).ToList();

            return Result.Ok(badgeDtos);
        }

        public Result<BadgeDto> readBadge(long badgeId)
        {
            var ex = _badgeRepository.getBadgeById(badgeId);
            if (ex != null)
            {
                ex.ReadBadge();
                var result = _badgeRepository.updateBadge(ex);
                return Result.Ok(MapToDto(result));
            }
            return null;


        }

        public Result<List<BadgeDto>> getAllById(long userId)
        {
            var badges = _badgeRepository.getAllById(userId);
            var badgeDtos = badges.Select(b => new BadgeDto
            {
                Id = b.Id,
                Level = (API.Dtos.BadgeDto.AchievementLevels)b.Level,
                Name = (API.Dtos.BadgeDto.BadgeName)b.Name,
                UserId = b.UserId,
                IsRead = b.IsRead,
            }).ToList();

            return Result.Ok(badgeDtos);
        }

        public Result<List<BadgeDto>> getAllNotReadById(long userId)
        {
            var badges = _badgeRepository.getAllNotReadById(userId);
            var badgeDtos = badges.Select(b => new BadgeDto
            {
                Id = b.Id,
                Level = (API.Dtos.BadgeDto.AchievementLevels)b.Level,
                Name = (API.Dtos.BadgeDto.BadgeName)b.Name,
                UserId = b.UserId,
            }).ToList();

            return Result.Ok(badgeDtos);
        }

        private readonly Dictionary<Domain.Tours.TourTags, Domain.Badge.BadgeName> _tagToBadgeMapping = new()
    {
        { Domain.Tours.TourTags.Culture, Domain.Badge.BadgeName.CulturalEnthusiast },
        { Domain.Tours.TourTags.Adventure, Domain.Badge.BadgeName.AdventureSeeker },
        { Domain.Tours.TourTags.Nature, Domain.Badge.BadgeName.NatureLover },
        { Domain.Tours.TourTags.CityTour, Domain.Badge.BadgeName.CityExplorer },
        { Domain.Tours.TourTags.Historical, Domain.Badge.BadgeName.HistoricalBuff },
        { Domain.Tours.TourTags.Relaxation, Domain.Badge.BadgeName.RelaxationGuru },
        { Domain.Tours.TourTags.Wildlife, Domain.Badge.BadgeName.WildlifeWanderer },
        { Domain.Tours.TourTags.Beach, Domain.Badge.BadgeName.BeachLover },
        { Domain.Tours.TourTags.Mountains, Domain.Badge.BadgeName.MountainConqueror },
        { Domain.Tours.TourTags.NightTour, Domain.Badge.BadgeName.PartyManiac }
    };
    }
}
