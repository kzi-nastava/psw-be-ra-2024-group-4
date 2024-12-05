namespace Explorer.Blog.API.Dtos
{
    public class AdvertisementDto
    {
        public long TouristId { get; set; }
        public long Id { get; set; }
        public long? TourId { get; set; }
        public long? ClubId { get; set; }
        public string Content { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
