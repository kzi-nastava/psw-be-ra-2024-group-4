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
        public ClubService(ICrudRepository<Club> repository, IMapper mapper,IUserRepository userRepository,IClubRepository clubRepository) : base(repository, mapper) 
        {
            _clubRepository= clubRepository;
            _userRepository= userRepository;
        }
    

    }
}
