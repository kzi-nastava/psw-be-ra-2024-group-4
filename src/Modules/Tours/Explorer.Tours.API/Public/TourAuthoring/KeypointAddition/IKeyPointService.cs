using Explorer.Tours.API.Dtos;
using FluentResults;


namespace Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;

public interface IKeyPointService
{
    Result<KeyPointDto> Create(KeyPointDto keyPointDto);
    Result<KeyPointDto> Update(KeyPointDto keyPointDto);
    Result Delete(int id);

    Result<List<KeyPointDto>> GetByUserId(long userId);

    Result<KeyPointDto> Get(int id);

}