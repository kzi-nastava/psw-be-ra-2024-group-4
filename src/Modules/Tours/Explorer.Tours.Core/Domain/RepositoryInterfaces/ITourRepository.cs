using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetToursByUserId(long userId);
        List<Equipment> GetEquipment(long tourId);  
        void AddEquipmentToTour(long tourId, long equipmentId);
        void RemoveEquipmentFromTour(long tourId, long equipmentId);
        Tour GetSpecificTourByUser(long id, long userId);
        public Tour GetById(long id);
        public void Save();
        PagedResult<Tour> GetPublished(int page, int pageSize);
        PagedResult<Tour> GetByKeyPoints(List<KeyPoint> keyPoints, int page, int pageSize);
        public Tour GetWithKeyPoints(int tourId);

    }
}
