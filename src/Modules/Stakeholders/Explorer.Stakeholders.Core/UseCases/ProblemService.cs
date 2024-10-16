using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemService: CrudService<ProblemDTO, Problem>, IProblemService
    {
        public IProblemRepository _problemRepository { get; set; }
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper, IProblemRepository problemRepository) : base(repository, mapper) {
            _problemRepository = problemRepository;
        }

        public Result<List<ProblemDTO>> GetByUserId(long id)
        {
            var problems = _problemRepository.GetByUserId(id);
            if (problems.Count == 0)
                return Result.Fail<List<ProblemDTO>>("No found problems");

            var problemsDTO = problems.Select(problem => new ProblemDTO
            {
                Id=problem.Id,
                UserId = problem.UserId,
                TourId = problem.TourId,
                Category = problem.Category,
                Description = problem.Description,
                Priority = problem.Priority,
                Time = problem.Time
            }).ToList();

            return Result.Ok(problemsDTO);

        }
        public Result<List<ProblemDTO>> GetByTourId(long id)
        {
            var problems = _problemRepository.GetByTourId(id);
            if (problems.Count == 0)
                return Result.Fail<List<ProblemDTO>>("No found problems");

            var problemsDTO = problems.Select(problem => new ProblemDTO
            {
                Id = problem.Id,
                UserId = problem.UserId,
                TourId = problem.TourId,
                Category = problem.Category,
                Description = problem.Description,
                Priority = problem.Priority,
                Time = problem.Time
            }).ToList();

            return Result.Ok(problemsDTO);
        }
    }
}
