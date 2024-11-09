using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        List<KeyPoint> GetAll();
        List<KeyPoint> GetKeyPointsByUserId(long userId);
        int GetMaxId(long userId);
        List<KeyPoint> GetAll();
    }
}
