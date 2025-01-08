using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class BadgeDto
    {
        public long Id { get; set; }
        public long UserId { get;  set; }

        public BadgeName Name { get; set; }
        public AchievementLevels Level { get; set; }

        public bool IsRead { get; set; }

        public enum BadgeName
        {
            ExplorerStep,
            Globetrotter,
            PhotoPro,
            TourTaster,
            SocialButterfly,
            TravelBuddy,
            CulturalEnthusiast,
            AdventureSeeker,
            NatureLover,
            CityExplorer,
            HistoricalBuff,
            RelaxationGuru,
            WildlifeWanderer,
            BeachLover,
            MountainConqueror,
            PartyManiac
        }
        public enum AchievementLevels
        {
            Bronze,
            Silver,
            Gold,
            None
        }
    }
}
