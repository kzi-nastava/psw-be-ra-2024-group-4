using Explorer.Stakeholders.API.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases
{

    public class PersoneUpdateService : CrudService<PersonUpdateDto, Person>, IPersonService
    {
        public PersoneUpdateService(ICrudRepository<Person> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
