using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonService : CrudService<PersonDto, Person>, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(ICrudRepository<Person> repository, IMapper mapper, IPersonRepository personRepository) : base(repository, mapper)
        {
            _personRepository = personRepository;
        }
        Result<PersonDto> IPersonService.Get(int id)
        {
            throw new NotImplementedException();
        }

      
    }
}
