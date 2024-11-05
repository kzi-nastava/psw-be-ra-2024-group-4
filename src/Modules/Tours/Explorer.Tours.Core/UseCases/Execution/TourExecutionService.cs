using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class TourExecutionService : BaseService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly ITourExecutionRepository tourExecutionRepository;
        IMapper _mapper { get; set; }

        public TourExecutionService(ITourExecutionRepository tourExecutionRepository, IMapper mapper) : base(mapper)
        {
            this.tourExecutionRepository = tourExecutionRepository;
            _mapper = mapper;
        }

        public Result<TourExecutionDto> Create(TourExecutionDto execution)
        {
            var ex = tourExecutionRepository.Create(MapToDomain(execution));
            if (ex != null) {
                ex.StartTourExecution();
                var result = tourExecutionRepository.Update(ex);
                if (result != null)
                {
                    return Result.Ok(MapToDto(result));
                }
                    

            }
            return null;

        }

        public Result<TourExecutionDto> CompleteTourExecution(long id)
        {
            var ex = tourExecutionRepository.Get(id);
            if (ex != null) {
                ex.CompleteTourExecution();
                var result = tourExecutionRepository.Update(ex);
                return Result.Ok(MapToDto(result));
            }
            return null;
            

            
        }

        public Result<TourExecutionDto> AbandonTourExecution(long id)
        {
            var ex = tourExecutionRepository.Get(id);
            if (ex != null)
            {
                ex.AbandonTourExecution();
                var result = tourExecutionRepository.Update(ex);
                return Result.Ok(MapToDto(result));
            }
            return null;
        }

        

        
    }
}
