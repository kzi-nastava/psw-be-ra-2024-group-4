using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.Core.Domain
{
    public enum EncounterStatus { Active, Draft, Archived }
    public enum EncounterType { Social, HiddenLocation, Misc }
    public class Encounter: Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public int XP { get; private set; }
        public EncounterStatus Status { get; private set; }
        public EncounterType Type { get; private set; }

        // Specifični podaci za različite tipove izazova
        public SocialData? SocialDetails { get; private set; }
        public HiddenLocationData? HiddenLocationDetails { get; private set; }
        public MiscData? MiscDetails { get; private set; }

        public Encounter() { }

        public Encounter(string title, string description, double longitude, double latitude, int xp, EncounterStatus status, EncounterType type)
        {
            Title = title;
            Description = description;
            Longitude = longitude; 
            Latitude = latitude;
            XP = xp;
            Status = status;
            Type = type;
        }
    }

    public class SocialData // npr potrebno je da je 5 ljudi na slicnoj razdaljini
    {
        public int RequiredParticipants { get; private set; }
        public double Radius { get; private set; }
    }

    public class HiddenLocationData // okaci sliku  sa neke lokacije
                                    // promeniti spram dalje implementacije
    {
        public string ImageUrl { get; private set; }
        public double ActivationRadius { get; private set; }
    }

    public class MiscData
    {
        public string ActionDescription { get; private set; }
    }
}
