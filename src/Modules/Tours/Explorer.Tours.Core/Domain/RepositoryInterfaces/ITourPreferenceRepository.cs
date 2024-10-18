using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces {
    public interface ITourPreferenceRepository {
        TourPreference Get(int id);
        Task<List<TourPreference>> GetAll();
        List<TourPreference> GetByTouristId(int touristId);
        Task Add(TourPreference preference);
    }
}
