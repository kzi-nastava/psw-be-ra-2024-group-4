﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TourStatus = Explorer.Tours.API.Dtos.TourStatus;
using TourTags = Explorer.Tours.API.Dtos.TourTags;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        //ova klasa ce biti podlozna promenama.
        //Za sada sam ovako uradila, ali kad potvrdim sa asistenom i kolegama mozda promenim.
        //Posto koleginica zavisi od toga kad cu ja zavrsiti ja cu predati sada.

        ITourRepository _tourRepository { get; set; }

        IKeyPointRepository _keyPointRepository { get; set; }
        IMapper _mapper { get; set; }

        IKeyPointService _keyPointService { get; set; }
        public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository, IKeyPointRepository keyPointRepository, IKeyPointService keyPointService) : base(repository, mapper)
        {
            _tourRepository = tourRepository;
            _keyPointRepository = keyPointRepository;
            _mapper = mapper;
            _keyPointService = keyPointService;
        }

        public Result<List<TourDto>> GetByUserId(long userId)
        {

            {
                var tours = _tourRepository.GetToursByUserId(userId);

                if (tours == null || tours.Count == 0)
                {
                    return Result.Fail<List<TourDto>>("No tours found for the specified user.");
                }

                 var tourDtos = tours.Select(t => new TourDto
                 {
                     Id = t.Id,
                     Name = t.Name,
                     Description = t.Description,
                     Difficulty = t.Difficulty,
                     Tags = t.Tags.Select(tag => (TourTags)tag).ToList(),
                     UserId = t.UserId,
                     Status = (TourStatus)t.Status,
                     Price = t.Price,
                     EquipmentIds = t.EquipmentIds,
                     KeyPoints = MapListToDto(t.KeyPoints),




                 }).ToList();

                return MapToDto(tours);

            }
        }

        private List<KeyPointDto> MapListToDto(List<KeyPoint> keyPoints)
        {
            var result = keyPoints.Select(kp => new KeyPointDto
            {
                Id = kp.Id,
                Name = kp.Name,
                Description = kp.Description,
                Longitude = kp.Longitude,
                Latitude = kp.Latitude,
                Image = kp.Image,
                UserId = kp.UserId,
            }).ToList();
            return result;
        }

        public Result<PagedResult<EquipmentDto>> GetEquipment(long tourId)
        {
            var equipment = _tourRepository.GetEquipment(tourId);
            var result = new PagedResult<Equipment>(equipment, equipment.Count);
            var items = result.Results.Select(_mapper.Map<EquipmentDto>).ToList();
            return new PagedResult<EquipmentDto>(items, result.TotalCount);
        }

        public Result AddEquipmentToTour(long tourId, long equipmentId)
        {
            try
            {
                _tourRepository.AddEquipmentToTour(tourId, equipmentId);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result RemoveEquipmentFromTour(long tourId, long equipmentId)
        {
            try
            {
                _tourRepository.RemoveEquipmentFromTour(tourId, equipmentId);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourDto> AddKeypointToTour(TourDto tour, KeyPointDto keypoint)
        {
            if (tour == null)
            {
                return Result.Fail("No tour found.");

            }

    

            if(keypoint == null)
            {
                return Result.Fail("No keypoint found.");
            }


            if (!tour.KeyPoints.Any(kp => kp.Id == keypoint.Id))
            {
                tour.KeyPoints.Add(keypoint);
                Update(tour);
                return Result.Ok(tour);
            }

            return Result.Fail("Keypoint already exists in this tour.");
        }
    }
}
