using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring.ObjectAddition
{
    public class ObjectService : CrudService<ObjectDTO, Domain.Object>, IObjectService
    {
        private readonly IObjectRepository _objectRepository;
      
        public ObjectService(ICrudRepository<Domain.Object> crudRepository, IMapper mapper, IObjectRepository objectRepository) : base(crudRepository, mapper)
        {
            _objectRepository = objectRepository;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371.0;

            double dLat = Math.PI / 180 * (lat2 - lat1);
            double dLon = Math.PI / 180 * (lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(Math.PI / 180 * lat1) * Math.Cos(Math.PI / 180 * lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        public Result<List<ObjectDTO>> GetInRadius(double radius, double lat, double lon)
        {
            var objects = _objectRepository.GetAll();

            var filteredObjects = objects.FindAll(obj =>
            {
                double distance = CalculateDistance(lat, lon, obj.Latitude, obj.Longitude);
                
                return distance <= radius && obj.PublicStatus == Domain.Tours.PublicStatus.PUBLIC;
            });

            List<ObjectDTO> objectDTOs = new();

            foreach (var obj in filteredObjects) 
            {
                objectDTOs.Add(MapToDto(obj));  
            }

            return Result.Ok(objectDTOs);
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
