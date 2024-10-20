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
    public class ClubJoinRequestService : CrudService<ClubJoinRequestDto, ClubJoinRequest>, IClubJoinRequestService
    {
        private readonly IClubJoinRequestRepository _clubJoinRequestRepository;
        public ClubJoinRequestService(ICrudRepository<ClubJoinRequest> repository, IMapper mapper, IClubJoinRequestRepository clubJoinRequestRepository) : base(repository, mapper) {
            _clubJoinRequestRepository = clubJoinRequestRepository;
        }
        public Result<List<ClubJoinRequestDto>> GetRequestsForClubMembers(int clubId)
        {
            var allRequests = _clubJoinRequestRepository.GetAll();

            var acceptedRequests = allRequests
            .Where(r => r.Status == Domain.JoinRequestStatus.ACCEPTED && r.ClubId == clubId)
            .ToList();

            return MapToDto(acceptedRequests);
        }
        public bool UserRequestExists(int clubId, int userId)
        {
            var request = _clubJoinRequestRepository.GetAll().FirstOrDefault(r => r.ClubId == clubId && r.UserId == userId);
            return request != null;

        }
    }

}
