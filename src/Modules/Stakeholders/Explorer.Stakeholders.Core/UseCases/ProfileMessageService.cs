using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
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

        public ProfileMessageService(IMapper mapper, IProfileMessageRepository profileMessageRepository) : base(mapper)
        {
            _profileMessageRepository = profileMessageRepository;
        }

        public Result<PagedResult<ProfileMessageDto>> GetByClubId(long clubId)
        {
            var messages = _profileMessageRepository.GetByClubId(clubId);
            return MapToDto(messages);
        }

        public Result<PagedResult<ProfileMessageDto>> GetByUserId(long userId)
        {
            var messages = _profileMessageRepository.GetByUserId(userId);
            return MapToDto(messages);
        }

        public Result Delete(long aggregateRootId)
        {
            var result = _profileMessageRepository.Delete(aggregateRootId);
            return result ? Result.Ok() : Result.Fail(FailureCode.NotFound);
        }

        public Result<ProfileMessageDto> Create(ProfileMessageDto profileMessage)
        {
            var newProfileMessage = _profileMessageRepository.Create(MapToDomain(profileMessage));
            return MapToDto(newProfileMessage);
        }

        public Result<ProfileMessageDto> Update(ProfileMessageDto aggregateRoot)
        {
            var newProfileMessage = _profileMessageRepository.Update(MapToDomain(aggregateRoot));
            return MapToDto(newProfileMessage);
        }
    }
}
