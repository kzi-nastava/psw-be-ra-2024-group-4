using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public enum TransportationType { OnFoot, Bicycle, Car }
    public class TourDurationDTO
    {
        public TransportationType Transportation { get; set; }
        public double Duration { get; set; }

        public TourDurationDTO(TransportationType transportation, double duration)
        {
            Transportation = transportation;
            Duration = duration;
        }

    }
}