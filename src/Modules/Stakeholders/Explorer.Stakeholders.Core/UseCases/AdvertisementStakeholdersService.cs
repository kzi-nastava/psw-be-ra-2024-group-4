using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AdvertisementStakeholdersService :  IAdvertisementStakeholdersService
    {
        private readonly IUserService _userService;
        private readonly IClubService _clubService;

        public AdvertisementStakeholdersService(IUserService userService, IClubService clubService) 
        {
            _userService = userService;
            _clubService = clubService;
        }

        public Result<List<ClubDto>> GetAllClubs()
        {
            return _clubService.GetPaged(0, 0).Value.Results;
        }

        public Result<List<UserDto>> GetAllTourists()
        {
            return _userService.GetPaged(0, 0).Value.Results.Where(t => t.Role == API.Dtos.UserRole.Tourist).ToList();
        }
    }
}
