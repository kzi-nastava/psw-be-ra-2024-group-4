using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProblemService
    {
        Result<ProblemDTO> Create(ProblemDTO problemDto);
        Result<ProblemDTO> Update(ProblemDTO problemDto);
        Result Delete(int id);
    }
}
