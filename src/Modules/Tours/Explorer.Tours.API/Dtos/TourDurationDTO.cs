using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDurationDTO
    {
        public string Transportation { get; set; }
        public double Duration { get; set; }
        public TourDurationDTO() { }
        public TourDurationDTO(string transportation, double duration)
        {
            Transportation = transportation;
            Duration = duration;
        }

    }
}