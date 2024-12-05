using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class AdvertisementService : CrudService<AdvertisementDto, Advertisement>, IAdvertisementService
    {
        private readonly ICrudRepository<Advertisement> _crudRepository;
        public IAdvertisementUserService _advertisementUserService;
        public AdvertisementService(ICrudRepository<Advertisement> crudRepository, IMapper mapper, IAdvertisementUserService advertisementUserService) : base(crudRepository, mapper)
        {
            _crudRepository = crudRepository;
            _advertisementUserService = advertisementUserService;
        }

        public Result<AdvertisementDto> Create()
        {
            throw new NotImplementedException();
        }

        public Result<List<AdvertisementDto>> GetByTourist(int touristId)
        {
            throw new NotImplementedException();
        }
    }
}
