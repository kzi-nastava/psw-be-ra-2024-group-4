using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourAuthoring.ObjectAddition
{
    public class ObjectService : CrudService<ObjectDTO, Explorer.Tours.Core.Domain.Object>, IObjectService
    {
        public ObjectService(ICrudRepository<Domain.Object> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
