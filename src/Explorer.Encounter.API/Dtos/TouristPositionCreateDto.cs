using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.API.Dtos
{
    public class TouristPositionCreateDto
    {
        public long TouristId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
