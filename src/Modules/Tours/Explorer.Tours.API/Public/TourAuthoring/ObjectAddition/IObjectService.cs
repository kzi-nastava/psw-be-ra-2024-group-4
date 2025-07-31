using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;

public interface IObjectService
{
    Result<PagedResult<ObjectDTO>> GetPaged(int pageNumber, int pageSize);
    Result<ObjectDTO> Create(ObjectDTO objectDTO);
    Result<ObjectDTO> Update(ObjectDTO objectDTO);
    Result Delete(int id);
    Result<List<ObjectDTO>> GetRequestedPublic();
    Result<List<ObjectDTO>> GetInRadius(double radius, double lat, double lng);
}

