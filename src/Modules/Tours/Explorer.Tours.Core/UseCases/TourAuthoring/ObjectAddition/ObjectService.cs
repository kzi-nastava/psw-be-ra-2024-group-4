using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring.ObjectAddition
{
    public class ObjectService : CrudService<ObjectDTO, Explorer.Tours.Core.Domain.Object>, IObjectService
    {
        private IObjectRepository _objectRepository;
        public ObjectService(ICrudRepository<Domain.Object> crudRepository, IMapper mapper, IObjectRepository objectRepository) : base(crudRepository, mapper)
        {
            _objectRepository = objectRepository;
        }

        public Result<List<ObjectDTO>> GetRequestedPublic()
        {
            var objects = _objectRepository.GetAll().FindAll(obj => obj.PublicStatus == Domain.Tours.PublicStatus.REQUESTED_PUBLIC);

            var objectDtos = objects.Select(obj => new ObjectDTO
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Image = obj.Image,
                Category = (ObjectCategory)obj.Category,
                Longitude = obj.Longitude,
                Latitude = obj.Latitude,
                UserId = obj.UserId,
                PublicStatus = (PublicStatus)obj.PublicStatus,
            }).ToList();

            return Result.Ok(objectDtos);
        }

    }
}
