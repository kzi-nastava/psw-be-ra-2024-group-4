using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourStatus = Explorer.Tours.API.Dtos.TourStatus;
using TourTags = Explorer.Tours.API.Dtos.TourTags;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {

        ITourRepository _tourRepository { get; set; }
        IMapper _mapper { get; set; }
        public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper) 
        {
            _tourRepository = tourRepository;
            _mapper = mapper;
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
                    KeyPointIds = t.KeyPointIds,




                }).ToList();

                return Result.Ok(tourDtos);

            }
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
        public Result<TourDto> AddKeyPoint(TourDto tour, long keyPointId)
        {


            if (tour == null)
            {
                return Result.Fail("No tour found.");

            }


            if (!tour.KeyPointIds.Contains(keyPointId))
            {
                tour.KeyPointIds.Add(keyPointId);
                Update(tour);
                return Result.Ok(tour);
            }

            return Result.Fail("Keypoint already exists in this tour.");





        }

        public Result Archive(long id, long authorId)
        {
            try
            {
                _tourRepository.Archive(id, authorId);
                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                return Result.Fail(FailureCode.Forbidden).WithError(e.Message);
            }
        }

        public Result Reactivate(long id, long authorId)
        {
            try
            {
                _tourRepository.Reactivate(id, authorId);
                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                return Result.Fail(FailureCode.Forbidden).WithError(e.Message);
            }
        }



    }
}
