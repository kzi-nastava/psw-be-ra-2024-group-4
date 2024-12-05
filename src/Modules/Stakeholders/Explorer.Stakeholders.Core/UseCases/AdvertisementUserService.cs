using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AdvertisementUserService : CrudService<UserDto, User>, IAdvertisementUserService
    {
        private readonly ICrudRepository<User> _repository;
        private readonly IClubService _clubService;

        public AdvertisementUserService(ICrudRepository<User> repository, IMapper mapper, IClubService clubService) : base(repository, mapper)
        {
            _repository = repository;
            _clubService = clubService;
        }

        public Result<List<ClubDto>> GetAllClubs()
        {
            return _clubService.GetPaged(0, 0).Value.Results;
        }

        public Result<List<UserDto>> GetAllUsers()
        {
            var results = _repository.GetPaged(0, 0);

            List<UserDto> userDtos = new List<UserDto>();
            foreach (var item in results.Results) 
            {
                userDtos.Add(MapToDto(item));
            }

            return userDtos;
        }
    }
}
