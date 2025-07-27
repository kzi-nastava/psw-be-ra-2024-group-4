﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourAuthoring
{
    public interface ITourService
    {
        Result<TourDto> Create(TourDto dto);
        Result<List<TourDto>> GetByUserId(long userId);
        Result<PagedResult<EquipmentDto>> GetEquipment(long tourId);
        Result AddEquipmentToTour(long tourId, List<long> equipmentIds);
        Result RemoveEquipmentFromTour(long tourId, long equipmentId);
        Result<TourDto> Get(int id);
        public Result GetById(long id);
        Result UpdateDistance(long id, double distance);
        Result Archive(long id);
        Result Publish(long id);
        Result Reactivate(long id);
        Result DeleteTour(int id);
        Result<TourDto> GetWithKeyPoints(int tourId);
        Result<TourDto> GetTourById(long id);
        Result<PagedResult<TourDto>> GetPublised();
    }
}
