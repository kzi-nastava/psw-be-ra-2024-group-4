using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos {
    public class TourPreferenceDto {
        public int? Id { get; set; }

        public int? TouristId { get; set; }
        public int WeightPreference { get; set; }
        public int WalkingRating { get; set; }
        public int BikeRating { get; set; }
        public int CarRating { get; set; }
        public int BoatRating { get; set; }
        public List<UserTags> Tags { get; set; }

        public enum UserTags
        {
            Cycling,
            Culture,
            Adventure,
            FamilyFriendly,
            Nature,
            CityTour,
            Historical,
            Relaxation,
            Wildlife,
            NightTour,
            Beach,
            Mountains,
            Photography,
            Guided,
            SelfGuided
        }
    }
}
