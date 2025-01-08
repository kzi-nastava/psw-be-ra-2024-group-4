using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly ToursContext _dbContext;

        public BadgeRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddBadgeIfNotExist(Badge.BadgeName name, Badge.AchievementLevels level, long userId)
        {
            var existingBadge = _dbContext.Badges
            .FirstOrDefault(b => b.Name == name && b.Level == level && b.UserId == userId);

            if (existingBadge != null)
            {
                
                return true;
            }
            Badge newBadge = new Badge(userId, name, level);
            _dbContext.Badges.Add(newBadge);
            _dbContext.SaveChanges();
            return false;

           
        }

        public List<Badge> getAll()
        {
           return _dbContext.Badges.ToList();
        }

        public List<Badge> getAllNotRead()
        {
            return _dbContext.Badges.Where(b => !b.IsRead).ToList();
        }

        public Badge getBadgeById(long id)
        {
            return _dbContext.Badges.FirstOrDefault(b => b.Id == id);
        }

        public Badge updateBadge(Badge updatedBadge)
        {
            _dbContext.Entry(updatedBadge).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return updatedBadge;
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }

        
    }
}
