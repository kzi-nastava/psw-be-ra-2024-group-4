using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        List<KeyPoint> GetKeyPointsByUserId(long userId);
        int GetMaxId(long userId);

        KeyPoint AddKeyPointToTour(long tourId, KeyPoint keyPoint);
    }
}
