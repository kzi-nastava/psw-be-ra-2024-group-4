﻿using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours;
public class KeyPoint : Entity
{

    public string Name { get; private set; }
    public double Longitude { get; private set; }
    public double Latitude { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }
    public PublicStatus PublicStatus { get; private set; }

    public long UserId { get; private set; }


    public long TourId { get; private set; }
    public KeyPoint(string name, double longitude, double latitude, string description, string image, long userId, long tourId, PublicStatus publicStatus)
    {
        Validate(name, longitude, latitude, description, image, userId,publicStatus);
        Name = name;
        Longitude = longitude;
        Latitude = latitude;
        Description = description;
        Image = image;
        UserId = userId;
        TourId = tourId;
        PublicStatus = publicStatus;
    }

    public void Validate(string name, double longitude, double latitude, string description, string image, long userId, PublicStatus publicStatus)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
        if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Invalid Image.");
        if (longitude <= 0) throw new ArgumentException("Invalid longitude.");
        if (latitude <= 0) throw new ArgumentException("Invalid latitude.");
        if (publicStatus < 0) throw new ArgumentException("Invalid status");
    }
}
public enum PublicStatus {
    PRIVATE = 0,
    REQUESTED_PUBLIC = 1,
    PUBLIC = 2
}


