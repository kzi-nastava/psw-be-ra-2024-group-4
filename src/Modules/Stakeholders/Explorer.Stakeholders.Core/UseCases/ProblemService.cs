using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
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
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
