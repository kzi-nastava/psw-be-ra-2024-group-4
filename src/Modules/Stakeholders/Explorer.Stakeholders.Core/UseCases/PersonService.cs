using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonService : CrudService<PersonDto, Person>, IPersonService
    {
        public PersonService(ICrudRepository<Person> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<PersonDto> UpdatePersonEquipment(PersonDto personDto, List<int> equipmentIds)
        {
            if(personDto == null) return Result.Fail(FailureCode.NotFound).WithError(new KeyNotFoundException().Message);

            personDto.EquipmentIds = equipmentIds;

            //Update(personDto); ??
            return Result.Ok(personDto);
        }
    }
}
