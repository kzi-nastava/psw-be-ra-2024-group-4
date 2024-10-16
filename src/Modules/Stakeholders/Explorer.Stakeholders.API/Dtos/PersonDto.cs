using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonDto
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string ImageUrl { get; set; }
        public string Biography { get; set; }
        public string Moto { get; set; }
        public List<int> EquipmentIds { get; set; }
    }
}
