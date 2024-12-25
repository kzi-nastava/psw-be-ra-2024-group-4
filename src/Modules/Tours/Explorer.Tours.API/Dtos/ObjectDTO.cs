namespace Explorer.Tours.API.Dtos;

public class ObjectDTO
{
    public long Id { get; set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public string Image { get; set; }
    public ObjectCategory Category { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public long UserId { get; set; }
    public PublicStatus PublicStatus { get; set; }
    public string ImageBase64 { get; set; }

    public ObjectDTO() { }

}

public enum ObjectCategory {
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
    Stadium,
    Fortress,
    Other
}
