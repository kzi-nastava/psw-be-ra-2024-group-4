using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain
{
    public enum ObjectCategory
    {
        Wc,
        Restaurant,
        Parking,
        Viewpoint,
        Church,
        Mosque,
        Bridge,
        Beach,
        Park,
        Fountain,
        ShoppingCenter,
        Museum,
        MarketPlace,
        NightClub,
        Other
    };


    public class Object: Entity
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public ObjectCategory Category { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public long UserId { get; private set; }
        public PublicStatus PublicStatus {  get; private set; }

        public Object(long id, string name, string description, string image, ObjectCategory category, double longitude, double latitude, long userId, PublicStatus publicStatus)
        {
            Validate(id, name, description, image, category, longitude, latitude, userId, publicStatus);
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Category = category;
            Longitude = longitude;
            Latitude = latitude;
            UserId = userId;
            PublicStatus = publicStatus;
        }

        public void Validate(long id, string name, string description, string image, ObjectCategory category, double longitude, double latitude, long userId, PublicStatus publicStatus)
        {
          

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Invalid Description.");

            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("Invalid Image.");

            if (longitude < -180 || longitude > 180)
                throw new ArgumentException("Longitude must be between -180 and 180 degrees.");

            if (latitude < -90 || latitude > 90)
                throw new ArgumentException("Latitude must be between -90 and 90 degrees.");

            if (!Enum.IsDefined(typeof(ObjectCategory), category))
                throw new ArgumentException("Invalid Category.");

            if (publicStatus < 0) 
                throw new ArgumentException("Invalid status");
        }

    }

}
