using Explorer.Blog.API.Dtos;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface IAdvertisementService
    {
        Result<List<AdvertisementDto>> GetByTourist(int touristId);
        //Result<AdvertisementDto> Create();
    }
}
