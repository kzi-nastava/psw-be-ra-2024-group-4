﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Geolocation;
using System.Drawing;

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
                UserId = kp.UserId



            }).ToList();

            return Result.Ok(keyPointDtos);

        }
    }

    public int GetMaxId(long userId)
    {
        return _keyPointRepository.GetMaxId(userId);
    }

    Result<List<KeyPointDto>> IKeyPointService.GetByCoordinated(long v1, long v2, int v3)
    {
        var res = new List<KeyPoint>();
        var centralCoordinate = new Coordinate(11.9, 18.9);
        var keyPoints = _keyPointRepository.GetAll();


        foreach (var kp in keyPoints)
        {
            double dist = GeoCalculator.GetDistance(
            centralCoordinate,
            new Coordinate(kp.Latitude, kp.Longitude),
            4,
            DistanceUnit.Kilometers);
            if (dist < 16)
                res.Add(kp);
        }

        var r = res.Select(kp => new KeyPointDto
        {
            Id = kp.Id,
            Name = kp.Name,
            Longitude = kp.Longitude,
            Latitude = kp.Latitude,
            Description = kp.Description,
            Image = kp.Image,
            UserId = kp.UserId



        }).ToList();

        return r;
    }
}