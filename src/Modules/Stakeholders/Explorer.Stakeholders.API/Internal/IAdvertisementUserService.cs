using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IAdvertisementUserService
    {
        Result<List<UserDto>> GetAllUsers();
        Result<List<ClubDto>> GetAllClubs();
    }
}
