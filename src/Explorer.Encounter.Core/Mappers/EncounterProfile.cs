using AutoMapper;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.Core.Mappers
{
    public class EncounterProfile : Profile
    {
        public EncounterProfile()
        {
            CreateMap<EncounterDto, Domain.Encounter>().ReverseMap();
        }
    }
}
