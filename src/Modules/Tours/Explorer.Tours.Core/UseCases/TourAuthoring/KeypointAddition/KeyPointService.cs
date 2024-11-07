using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring.KeypointAddition;

public class KeyPointService : CrudService<KeyPointDto, KeyPoint>, IKeyPointService
{
    IKeyPointRepository _keyPointRepository { get; set; }
    public KeyPointService(ICrudRepository<KeyPoint> repository, IMapper mapper, IKeyPointRepository keyPointRepository) : base(repository, mapper) {
        _keyPointRepository = keyPointRepository;
    }
    public Result<List<KeyPointDto>> GetByUserId(long userId)
    {

        {
            var keyPoints = _keyPointRepository.GetKeyPointsByUserId(userId);

            if (keyPoints == null || keyPoints.Count == 0)
            {
                return Result.Fail<List<KeyPointDto>>("No keypoints found for the specified user.");
            }

            var keyPointDtos = keyPoints.Select(kp => new KeyPointDto
            {
                Id = kp.Id,
                Name = kp.Name,
                Longitude = kp.Longitude,
                Latitude = kp.Latitude,
                Description = kp.Description,
                Image = kp.Image,
                UserId = kp.UserId,
                PublicStatus = (API.Dtos.PublicStatus)kp.PublicStatus,
                TourId = kp.TourId,



            }).ToList();

            return Result.Ok(keyPointDtos);

        }
    }

    public int GetMaxId(long userId)
    {
        return _keyPointRepository.GetMaxId(userId);
    }

    public Result<List<KeyPointDto>> GetRequestedPublic()
    {
        var keyPoints = _keyPointRepository.GetAll().FindAll(kp => kp.PublicStatus == Domain.Tours.PublicStatus.REQUESTED_PUBLIC);

        var keyPointDtos = keyPoints.Select(kp => new KeyPointDto
        {
            Id = kp.Id,
            Name = kp.Name,
            Longitude = kp.Longitude,
            Latitude = kp.Latitude,
            Description = kp.Description,
            Image = kp.Image,
            UserId = kp.UserId,
            PublicStatus = (API.Dtos.PublicStatus)kp.PublicStatus



        }).ToList();

        return Result.Ok(keyPointDtos);
    }
}