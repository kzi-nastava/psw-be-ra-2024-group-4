using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProfileMessageService : BaseService<ProfileMessageDto, ProfileMessage>, IProfileMessageService
    {
        public IProfileMessageRepository _profileMessageRepository { get; set; }

        public ProfileMessageService(IMapper mapper) : base(mapper)
        {
        }

        public Result<ProfileMessageDto> Create(ProfileMessageDto profileMessage)
        {
            throw new NotImplementedException();
        }

        public Result Delete(ProfileMessageDto aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public Result<PagedResult<ProfileMessageDto>> GetByClubId(long clubId)
        {
            throw new NotImplementedException();
        }

        public Result<PagedResult<ProfileMessageDto>> GetByUserId(long userId)
        {
            throw new NotImplementedException();
        }

        public Result<ProfileMessageDto> Update(ProfileMessageDto aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}
