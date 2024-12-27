using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task Add(TourPreference tourPreference) {
            var maxId = _context.TourPreferences.Max(tp => (int?)tp.Id) ?? 0;
            tourPreference.Id = maxId + 1;
            await _context.TourPreferences.AddAsync(tourPreference);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TourPreference>> GetAll() {
            return await _context.TourPreferences.ToListAsync();
        }

        public TourPreference Get(int id) {
            return _context.TourPreferences.Find(id);
        }
        public List<TourPreference> GetByTouristId(int touristId) {
            return _context.TourPreferences.Where(tp => tp.TouristId == touristId).ToList();
        }
        public void Update(TourPreference preference) {
            _context.TourPreferences.Update(preference);
            _context.SaveChanges();
        }


    }
}
