using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain {
    public class TourPreference : Entity {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int WeightPreference { get; set; }
        public int WalkingRating { get; set; }
        public int BikeRating { get; set; }
        public int CarRating { get; set; }
        public int BoatRating { get; set; }
        public List<string> Tags { get; set; }
    }
}
