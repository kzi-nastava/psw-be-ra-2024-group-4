using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;

public class TourKeypointDto
{
    public TourDto Tour { get; set; }
    public KeyPointDto KeyPoint { get; set; }

    public TourKeypointDto() { }
}
