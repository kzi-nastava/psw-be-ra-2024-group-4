using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories {
    public class TourPreferenceRepository : ITourPreferenceRepository {
        private readonly ToursContext _context;

        public TourPreferenceRepository(ToursContext context) {
            _context = context;
        }

        public TourPreference Get(int id) {
            return _context.TourPreferences.Find(id);
        }
        public List<TourPreference> GetByTouristId(int touristId) {
            return _context.TourPreferences.Where(tp => tp.TouristId == touristId).ToList();
        }

    }
}
