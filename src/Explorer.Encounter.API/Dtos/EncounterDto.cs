using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.API.Dtos
{
    namespace Explorer.Encounters.API.Dtos
    {
        public enum EncounterStatus { Active, Draft, Archived }
        public enum EncounterType { Social, HiddenLocation, Misc }
        public class EncounterDto
        {
            public long Id { get; set; } 
            public string Title { get; set; } 
            public string Description { get; set; }
            public double Latitude { get; set; } 
            public double Longitude { get; set; } 
            public int XP { get; set; }
            public EncounterStatus Status { get; set; } 
            public EncounterType Type { get; set; } 
            public SocialDataDto? SocialData { get; set; }
            public HiddenLocationDataDto? HiddenLocationData { get; set; }
            public MiscDataDto? MiscData { get; set; }
            public List<EncounterInstanceDto> Instances { get; set; }
        }
    }

}
