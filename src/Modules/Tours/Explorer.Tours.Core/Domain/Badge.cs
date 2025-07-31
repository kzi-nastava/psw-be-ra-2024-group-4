using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Badge : Entity
    {
        public long UserId { get; private set; }

        public BadgeName Name { get; private set; }
        public AchievementLevels Level { get; private set; }

        public bool IsRead { get; private set; }

        public Badge(long userId, BadgeName name, AchievementLevels level) { 
            UserId = userId;
            Name = name;
            Level = level;
            IsRead = false;
        }

        public enum BadgeName
        {
            ExplorerStep,
            CulturalEnthusiast,
            AdventureSeeker,
            NatureLover,
            CityExplorer,
            HistoricalBuff,
            RelaxationGuru,
            WildlifeWanderer,
            BeachLover,
            MountainConqueror,
            Globetrotter,
            PhotoPro,
            TourTaster,
            SocialButterfly,
            TravelBuddy,
            PartyManiac
        }
        public enum AchievementLevels
        {
            Bronze,
            Silver,
            Gold,
            None
        }

        public void ReadBadge()
        {
            IsRead = true;
        }
    }
}
