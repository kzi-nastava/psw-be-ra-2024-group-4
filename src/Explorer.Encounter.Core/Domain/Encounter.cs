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
        public SocialData? SocialData { get; private set; }
        public HiddenLocationData? HiddenLocationData { get; private set; }
        public MiscData? MiscData { get; private set; }
        public List<EncounterInstance>? Instances { get; } = new List<EncounterInstance>();
        public bool IsRequired {  get; private set; }

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

        public void ActivateEncounter(long userId, double userLongitude, double userLatitude)
        {
            if (Status != EncounterStatus.Active)
                throw new ArgumentException("Encounter is not yet published.");
            if (hasUserActivatedEncounter(userId))
                throw new ArgumentException("User has already activated/completed this encounter.");
            if (!isUserInRange(userLongitude, userLatitude))
                throw new ArgumentException("User is not close enough to the encounter.");

            Instances.Add(new EncounterInstance(userId));
        }

        public void CompleteEncounter(long userId)
        {
            if (Status != EncounterStatus.Active)
            {
                throw new InvalidOperationException("Cannot complete encounter because it is not active.");
            }

            var instance = Instances.FirstOrDefault(x => x.UserId == userId);
            if (instance == null)
            {
                throw new ArgumentException("Invalid user id.");
            }

            if (SocialData != null)
            {
                try
                {
                    SocialData.ValidateCompletion(Instances, userId);

                    foreach (var activeInstance in Instances.Where(i => i.Status == EncounterInstanceStatus.Active))
                    {
                        activeInstance.Complete();
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidOperationException("Not enough participants to complete the encounter.", ex);
                }
            }
            else
            {
                instance.Complete();
            }
        }


        protected bool hasUserActivatedEncounter(long userId)
        {
            return Instances.FirstOrDefault(x => x.UserId == userId) != default(EncounterInstance);
        }


        protected bool isUserInRange(double userLongitude, double userLatitude)
        {
            if (userLongitude < -180 || userLongitude > 180)
                throw new ArgumentException("Invalid Longitude");
            if (userLatitude < -90 || userLatitude > 90)
                throw new ArgumentException("Invalid Latitude");

            double radius;
            if (SocialData != null)
            {
                radius = SocialData.Radius;
            }
            else if (HiddenLocationData != null)
            {
                radius = HiddenLocationData.ActivationRadius;
            }
            else
            {
                throw new InvalidOperationException("Radius is not defined for this type of encounter.");
            }

            const double earthRadius = 6371000; 
            double latitude1 = Latitude * Math.PI / 180;
            double longitude1 = Longitude * Math.PI / 180;
            double latitude2 = userLatitude * Math.PI / 180;
            double longitude2 = userLongitude * Math.PI / 180;

            double latitudeDistance = latitude2 - latitude1;
            double longitudeDistance = longitude2 - longitude1;

            double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) *
                       Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = earthRadius * c;

            return distance < radius;
        }

    }

    public class SocialData // npr potrebno je da je 5 ljudi na slicnoj razdaljini
    {
        public int RequiredParticipants { get; private set; }
        public double Radius { get; private set; }

        public void ValidateCompletion(List<EncounterInstance> instances, long userId)
        {
            var encounterInstance = instances.FirstOrDefault(x => x.UserId == userId);
            if (encounterInstance == null)
            {
                throw new ArgumentException("Invalid user id.");
            }

            var activatedInstances = instances.Count(i => i.Status == EncounterInstanceStatus.Active);

            if (activatedInstances < RequiredParticipants)
            {
                throw new ArgumentException("Not enough users that activated the social encounter.");
            }
        }
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
