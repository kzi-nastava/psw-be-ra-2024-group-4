using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProfileMessageService
    {
        Result<ProfileMessageDto> Create(ProfileMessageDto profileMessage);
        Result<PagedResult<ProfileMessageDto>> GetByUserId(long userId);
        Result<PagedResult<ProfileMessageDto>> GetByClubId(long clubId);
        Result<ProfileMessageDto> Update(ProfileMessageDto aggregateRoot);
        Result Delete(long aggregateRoot);
    }
}
