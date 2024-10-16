using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourStatus = Explorer.Tours.API.Dtos.TourStatus;
using TourTags = Explorer.Tours.API.Dtos.TourTags;

namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        //ova klasa ce biti podlozna promenama.
        //Za sada sam ovako uradila, ali kad potvrdim sa asistenom i kolegama mozda promenim.
        //Posto koleginica zavisi od toga kad cu ja zavrsiti ja cu predati sada.

        ITourRepository _tourRepository { get; set; }
        public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper) {
            _tourRepository = tourRepository;
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

        public Result<TourDto> AddKeyPoint(TourDto tour, long keyPointId)
        {
         

            if(tour == null)
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
    }
}
