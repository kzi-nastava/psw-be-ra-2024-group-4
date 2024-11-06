using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;
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
        private readonly IMapper _mapper;
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper, IProblemRepository problemRepository) : base(repository, mapper) {
            _problemRepository = problemRepository;
            _mapper = mapper;
        }

        public Result<List<ProblemDTO>> GetByTouristId(long id)
        {
            var problems = _problemRepository.GetByUserId(id);
            //ako vracas Result.Fail kada je count 0 imaces bug na frontu: ako imas 1 problem i obrises ga, bice problem jer mu vraca code 500. radi kada vratis praznu listu
            /*if (problems.Count == 0)
                return Result.Fail<List<ProblemDTO>>("No found problems");*/


            return MapToDto(problems);

        }
        public Result<List<ProblemDTO>> GetByTourId(long id)
        {
            var problems = _problemRepository.GetByTourId(id);
            /*if (problems.Count == 0)
                return Result.Fail<List<ProblemDTO>>("No found problems");*/

            return MapToDto(problems);
        }
        public Result<ProblemDTO> PostComment(ProblemCommentDto commentDto)
        {
            //kako mapirati 
            //var problem = _problemRepository.PostComment(new ProblemComment(commentDto.ProblemId, commentDto.UserId, commentDto.Text, commentDto.TimeSent));
            // var problem = _problemRepository.PostComment(_mapper.Map<ProblemCommentDto, ProblemComment>(commentDto));
            //problemrepo.getbyid
            var problem = _problemRepository.GetById(commentDto.ProblemId);

            // problem.PostComment()
            problem.PostComment(_mapper.Map<ProblemCommentDto, ProblemComment>(commentDto));
           //problemrepo.Update(problem)
           _problemRepository.Update(problem);

            if(problem == null)
            {
                return Result.Fail(FailureCode.NotFound).WithError($"Problem with ID {commentDto.ProblemId} not found.");
            }
           // return Result.Ok();
            return MapToDto(problem);
        }

        public Result<ProblemDTO> UpdateActiveStatus(long id, bool isActive)
        {
            var problem = _problemRepository.GetById(id);
            if (problem == null)
            {
                  return Result.Fail<ProblemDTO>("Problem not found.");
            }

            // Ažuriraj status
            problem.IsActive = isActive;

            // Praćenje i update kroz kontekst
            _problemRepository.Update(problem);

            return Result.Ok(MapToDto(problem));
        }

        public Result<ProblemDTO> GetById(long id)
        {
            var problem = _problemRepository.GetById(id);

            if (problem == null)
            {
                return Result.Fail<ProblemDTO>("Problem not found.");
            }

            return MapToDto(problem);  
        }

        public Result DeleteProblem(int id)
        {
            return Delete(id);
        }

    }
}
