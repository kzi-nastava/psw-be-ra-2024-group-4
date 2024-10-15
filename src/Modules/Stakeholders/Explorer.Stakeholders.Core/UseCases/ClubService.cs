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
    public class ClubService:CrudService<ClubDto,Club>,IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IUserRepository _userRepository;

       

        public ClubService(ICrudRepository<Club> repository, IMapper mapper,IUserRepository userRepository, IClubRepository clubRepository) : base(repository, mapper) 
        {
            _clubRepository= clubRepository;
            _userRepository= userRepository;
            

        }
        //public Result<ClubDto> Update(ClubDto clubDto, long userId)
        //{
        //    var club = CrudRepository.Get(clubDto.Id);
        //    if (club.UserId != userId)
        //    {
        //        return Result.Fail("Only the owner can update the club.");
        //    }

        //    return base.Update(clubDto); 
        //}

        //public Result<ClubDto> Create(ClubDto clubDto, string username)
        //{
        //    var user = _userRepository.GetActiveByName(username);
        //    if (user.Role != UserRole.Tourist)
        //    {
        //        return Result.Fail("Only tourists can create a club.");
        //    }

        //    return base.Create(clubDto); 
        //}

        //public Result<ClubDto> GetAll()
        //{
        //    var clubs= _clubRepository.GetAll();

        //}


        public Result DeleteMember(long memberId, int clubId, int userId)
        {
            try
            {
                
                var club = CrudRepository.Get(clubId);
                if (club == null)
                {
                    return Result.Fail(FailureCode.NotFound)
                                 .WithError($"Club with ID {clubId} not found.");
                }

                if (club.UserId != userId)
                {
                    return Result.Fail(FailureCode.Forbidden)
                                 .WithError("Only the owner of the club can remove members.");
                }

                bool removed = club.UserIds.Remove(memberId);
                if (!removed)
                {
                    return Result.Fail(FailureCode.NotFound)
                                 .WithError($"Member with ID {memberId} not found in the club.");
                }

                
                var updatedClub = CrudRepository.Update(club);

               
               // var clubDto = _mapper.Map<ClubDto>(updatedClub);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail("Result failed");
            }
        }


    }
}
