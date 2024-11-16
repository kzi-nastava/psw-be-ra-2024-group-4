using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions;

public class PositionSimulator : Entity
{
    public double Longitude { get; private set; }
    public double Latitude { get; private set; }

    public long TouristId { get; private set; }

    public PositionSimulator(double latitude, double longitude, long touristId)
    {
        Validate(latitude, longitude);
        Latitude = latitude;
        Longitude = longitude;
        TouristId = touristId;
        
    }

    private void Validate(double latitude, double longitude)
    {
        if (latitude <= 0) throw new ArgumentException("Invalid latitude");
        if (longitude <= 0) throw new ArgumentException("Invalid longitude");

    }
}
