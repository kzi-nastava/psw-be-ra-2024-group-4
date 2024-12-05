using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    internal class AdvertisementTourService : CrudService<TourDto, Tour>, IAdvertisementTourService
    {
        ICrudRepository<Tour> _repository;
        public AdvertisementTourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper) 
        {
            _repository = repository;
        }

    }
}
