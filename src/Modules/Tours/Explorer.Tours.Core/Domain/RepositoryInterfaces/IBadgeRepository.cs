using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IBadgeRepository
    {
        bool AddBadgeIfNotExist(Badge.BadgeName name, Badge.AchievementLevels level, long userId);
        List<Badge> getAll();

        List<Badge> getAllNotRead();

        Badge getBadgeById(long id);

        Badge updateBadge(Badge updatedBadge);
        void Save();
        List<Badge> getAllById(long userId);
        List<Badge> getAllNotReadById(long userId);
    }
}
