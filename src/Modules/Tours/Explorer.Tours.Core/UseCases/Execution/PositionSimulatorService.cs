using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution;

public class PositionSimulatorService : CrudService<PositionSimulatorDto, PositionSimulator>, IPositionSimulatorService
{
    IPositionSimulatorRepository _positionSimulatorRepository { get; set; }
    public PositionSimulatorService(IPositionSimulatorRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _positionSimulatorRepository = repository;
    }


    public Result<PositionSimulatorDto> GetByTouristId(long touristId)
    {
        var positionDto = MapToDto(_positionSimulatorRepository.GetByTouristId(touristId));

        if(positionDto == null)
        {
            return Result.Fail("No position found");
        }

        return Result.Ok(positionDto);
    }
}
