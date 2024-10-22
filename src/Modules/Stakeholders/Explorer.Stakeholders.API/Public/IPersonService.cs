using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IPersonService
    {
        Result<PersonUpdateDto> Update(PersonUpdateDto personUpdate);
        Result<PersonUpdateDto> Get(int id);

    }
}
