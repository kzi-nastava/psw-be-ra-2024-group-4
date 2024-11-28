namespace Explorer.Encounter.API.Dtos
{
    public class SocialDataDto
    {
        public int RequiredParticipants { get; set; } 
        public double Radius { get; set; } 
    }

    public class HiddenLocationDataDto
    {
        public string ImageUrl { get; set; } // URL slike lokacije
        public string ImageBase64 {  get; set; }
        public double ActivationRadius { get; set; } // Radijus za aktivaciju izazova
    }
    
    public class MiscDataDto
    {
        public string ActionDescription { get; set; } // Opis akcije koju korisnik treba da uradi
    }

}
