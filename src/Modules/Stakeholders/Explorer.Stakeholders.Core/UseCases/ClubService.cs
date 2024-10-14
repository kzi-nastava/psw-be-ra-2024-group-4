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
        public ClubService(ICrudRepository<Club> repository, IMapper mapper,IClubRepository clubRepository,IUserRepository userRepository) : base(repository, mapper) 
        {
            _clubRepository= clubRepository;
            _userRepository= userRepository;

        }
        //public  Result<ClubDto> Update(ClubDto clubDto, long userId)
        //{
        //    var club = CrudRepository.Get(clubDto.Id);

        //    // Check if the user is the owner of the club
        //    if (club.UserId != userId)
        //    {
        //        return Result.Fail("Only the owner can update the club.");
        //    }

        //    return base.Update(clubDto); // Call the base method to perform the update
        //}

        //public Result<ClubDto> Create(ClubDto clubDto, long userId)
        //{
        //    // Check if the user is a tourist
        //    var user = _userRepository.GetActiveById(userId); // Assumes you have a method to get active user by ID
        //    if (user.Role != UserRole.Tourist)
        //    {
        //        return Result.Fail("Only tourists can create a club.");
        //    }

        //    return base.Create(clubDto); // Call the base method to perform the create
        //}



    }
}
