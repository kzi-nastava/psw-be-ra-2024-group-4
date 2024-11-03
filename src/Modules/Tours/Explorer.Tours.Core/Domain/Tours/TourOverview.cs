using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TourOverview
    {
        public long TourId { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string? TourDifficulty { get; set; }
        public List<TourTags> Tags { get; set; }
        public KeyPoint FirstKeyPoint { get; set; }
        public List<TourReview> Reviews { get; set; }

        public TourOverview() { }
        public TourOverview(long tourId, string tourName, string tourDescription, string tourDifficulty, List<TourTags> tags, KeyPoint firstKeyPoint, List<TourReview> reviews)
        {
            TourId = tourId;
            TourName = tourName;
            TourDescription = tourDescription;
            TourDifficulty = tourDifficulty;
            Tags = tags;
            FirstKeyPoint = firstKeyPoint;
            Reviews = reviews;
        }
    }
}
