using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ToursContext _dbContext;

        public TourRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Tour> GetToursByUserId(long userId)
        {
            return _dbContext.Tour
                         .Where(t => t.UserId == userId)
                         .ToList();
        }


        public List<Equipment> GetEquipment(long tourId) 
        {
            var tour = _dbContext.Tour
                .FirstOrDefault(t => t.Id == tourId); 

            if (tour == null)
            {
                return new List<Equipment>(); 
            }

            var equipmentList = _dbContext.Equipment
                .Where(e => tour.EquipmentIds.Contains(e.Id)) 
                .ToList();

            return equipmentList;
        }


        public void AddEquipmentToTour(long tourId, long equipmentId)
        {
            var tour = _dbContext.Tour
                                .Single(t => t.Id == tourId);
            var equipment = _dbContext.Equipment
                                     .Single(e => e.Id == equipmentId);

            if (tour.EquipmentIds.Contains(equipment.Id))
            {
                throw new InvalidOperationException("This equipment is already added to the tour.");
            }
            tour.EquipmentIds.Add(equipment.Id);

            _dbContext.SaveChanges();
        }
        public void RemoveEquipmentFromTour(long tourId, long equipmentId)
        {
            var tour = _dbContext.Tour
                                .Single(t => t.Id == tourId);
            var equipment = _dbContext.Equipment
                                     .Single(e => e.Id == equipmentId);

            if (!tour.EquipmentIds.Contains(equipmentId))
            {
                throw new InvalidOperationException("This equipment is not associated with the tour.");
            }
            tour.EquipmentIds.Remove(equipment.Id);
            _dbContext.SaveChanges();
        }

        public Tour GetSpecificTourByUser(long tourId, long userId)
        {
            return _dbContext.Tour.SingleOrDefault(t => t.Id == tourId && t.UserId == userId);

        }
    }
}
