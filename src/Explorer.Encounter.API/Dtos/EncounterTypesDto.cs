using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public double ActivationRadius { get; set; } // Radijus za aktivaciju izazova
    }
    public class MiscDataDto
    {
        public string ActionDescription { get; set; } // Opis akcije koju korisnik treba da uradi
    }



}
