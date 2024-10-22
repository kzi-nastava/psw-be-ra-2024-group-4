﻿namespace Explorer.Tours.API.Dtos;
public class KeyPointDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public long UserId { get; set; }

    public string ImageBase64 { get; set; }
    public KeyPointDto() { }

}
