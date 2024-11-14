using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Explorer.Tours.Core.Domain.Tours
{

    public class TourDuration : ValueObject
    {
        
        public string Transportation { get; private set; }
        public double Duration { get; private set; }

        [JsonConstructor]
        public TourDuration(string transportation, double duration)
        {
            if (duration < 0)
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