using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Explorer.Tours.Core.Domain.Tours
{
    public enum TransportationType { OnFoot, Bicycle, Car }

    public class TourDuration : ValueObject
    {
        public TransportationType Transportation { get; private set; }
        public double Duration { get; private set; }

        [JsonConstructor]
        public TourDuration(TransportationType transportation, double duration)
        {
            if (duration <= 0)
                throw new ArgumentException("Duration must be a positive value.");

            Transportation = transportation;
            Duration = duration;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Transportation;
            yield return Duration;
        }
    }
}