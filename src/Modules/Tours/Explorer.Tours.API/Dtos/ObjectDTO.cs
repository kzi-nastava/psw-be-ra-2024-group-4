namespace Explorer.Tours.API.Dtos;

public class ObjectDTO
{
    public int Id { get; set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public string Image { get; set; }
    public ObjectCategory Category { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public long UserId { get; set; }

    public ObjectDTO() { }

}

public enum ObjectCategory { WC, Restaurant, Parking, Other }
