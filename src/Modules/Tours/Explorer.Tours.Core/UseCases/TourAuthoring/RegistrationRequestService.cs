using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class RegistrationRequestService : CrudService<RegistrationRequestDto, RegistrationRequest>, IRegistrationRequestService
    {
        public RegistrationRequestService(ICrudRepository<RegistrationRequest> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
