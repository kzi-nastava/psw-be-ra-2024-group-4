using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IBadgeRepository
    {
        bool AddBadgeIfExist(Badge.BadgeName name, Badge.AchievementLevels level, long userId);

        void Save();
    }
}
