using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<Tour> _dbSet;

        public TourRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Tour>();
        }

        public Tour GetById(long id)
        {
             var tour = _dbSet.FirstOrDefault(t => t.Id == id);
             if (tour == null)
             { 
                 throw new ArgumentException("Tour not found.");
             }
             return tour;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public List<Tour> GetToursByUserId(long userId)
        {
         
            return _dbContext.Tour.Include(t => t.KeyPoints)
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


        public PagedResult<Tour> GetPublished(int page, int pageSize)
        {
            var query = _dbContext.Tour
                .Include(t => t.KeyPoints)
                .Where(t => t.Status == TourStatus.Published)
                .Skip((page - 1) * pageSize);

            if (pageSize > 0)
            {
                query = query.Take(pageSize);
            }

            var ret = query.ToList();

            return new PagedResult<Tour>(ret, ret.Count());
        }

        public PagedResult<Tour> GetByKeyPoints(List<KeyPoint> keyPoints, int page, int pageSize)
        {
            var keyPointIdsToMatch = keyPoints.Select(kp => kp.Id).ToHashSet();
         
            var res=_dbContext.Tour.Include(t => t.KeyPoints)
                .Where(tour => tour.KeyPoints.Any(kp => keyPointIdsToMatch.Contains(kp.Id)))
                .Skip((page - 1) * pageSize);

            if (pageSize > 0)
            {
                res = res.Take(pageSize);
            }

            var ret = res.ToList();

            return new PagedResult<Tour>(ret, ret.Count());
        }
    }
}
