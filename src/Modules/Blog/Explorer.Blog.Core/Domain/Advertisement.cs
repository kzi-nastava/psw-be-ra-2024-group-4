using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public class Advertisement : Entity
    {
        public long TouristId { get; private set; }
        public long? TourId { get; private set; }
        public long? ClubId { get; private set; }
        public string Content { get; private set; }
        public DateTime ValidTo { get; private set; }

        public Advertisement(long touristId, long tourId, long clubId, string content, DateTime validTo) 
        {
            TouristId = touristId;
            TourId = tourId;
            ClubId = clubId;
            Content = content;
            ValidTo = validTo;
            Validate();
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId.");
            if (TourId == 0) throw new ArgumentException("Invalid TourId.");
            if (ClubId == 0) throw new ArgumentException("Invalid ClubId.");
            if (string.IsNullOrEmpty(Content)) throw new ArgumentException("Content can not be empty");
        }
    }
}
