
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;
public class KeyPoint : Entity
{
        public string Name { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public string Description { get; private set; }
        public string Image {  get; private set; }

        public KeyPoint(string name, double longitude, double latitude, string description, string image)
        {
            Validate(name, longitude, latitude, description, image);
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
            Description = description;
            Image = image;

            
        }

        public void Validate(string name, double longitude, double latitude, string description, string image)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Invalid Image.");
            if (longitude <= 0) throw new ArgumentException("Invalid longitude.");
            if (latitude <= 0) throw new ArgumentException("Invalid latitude.");
        }


}

