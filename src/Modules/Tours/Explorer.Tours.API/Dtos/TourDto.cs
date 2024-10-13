using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? Difficulty { get; set; }

        public List<TourTags> Tags { get;  set; }
        public TourStatus Status { get; set; }
        public double Price { get;  set; }
        public long UserId { get;  set; }

        public List<long> EquipmentIds { get; set; }

        public List<long> KeyPointIds { get; set; }

        public TourDto() { }

        public TourDto(string name, string? description, string? difficulty, List<TourTags> tags, long userId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            Difficulty = difficulty;
            if (tags == null || tags.Count == 0)
            { tags = new List<TourTags>(); }
            Tags = tags;
            if (userId <= 0)
                throw new ArgumentException("Invalid UserId. UserId must be a positive number.");
            UserId = userId;
            Status = TourStatus.Draft;
            Price = 0;
            EquipmentIds = new List<long>();
            KeyPointIds = new List<long>();
        }
    }

    public enum TourStatus
    {
        Draft,
        Published
    }

    public enum TourTags
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
