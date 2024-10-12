using Explorer.Tours.API.Dtos;
using FluentResults;


namespace Explorer.Tours.API.Public.Author;

public interface IKeyPointService
{
    Result<KeyPointDto> Create(KeyPointDto keyPointDto);
    Result<KeyPointDto> Update(KeyPointDto keyPointDto);
    Result Delete(int id);

}