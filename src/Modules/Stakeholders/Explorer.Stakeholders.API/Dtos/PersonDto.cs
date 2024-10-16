using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonDto
    {
        public long Id { get; set; }
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
    }
}
