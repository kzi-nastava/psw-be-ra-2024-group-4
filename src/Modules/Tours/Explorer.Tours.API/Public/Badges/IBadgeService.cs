using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Badges
{
    public interface IBadgeService 
    {
        Result AddBadgeIfNeeded(long tourId, long userId);

        Result<List<BadgeDto>> getAll();
        Result<List<BadgeDto>> getAllNotRead();
        Result<BadgeDto> readBadge(long badgeId);
    }
}
