namespace Explorer.Blog.API.Dtos
{
    public class AdvertisementDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long? TourId { get; set; }
        public long? ClubId { get; set; }
        public DateTime ValidTo { get; set; }

        public AdvertisementDto(long touristId, long tourId, long clubId, DateTime validTo) 
        {
            TouristId = touristId;
            TourId = tourId;
            ClubId = clubId;
            ValidTo = validTo;
        }
    }
}
