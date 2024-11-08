using AutoMapper;
using Explorer.Stakeholders.API.Dtos;

using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using System.Linq;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<AppReviewDto, AppReview>().ReverseMap();
        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<ProblemDTO, Problem>().ReverseMap();
        CreateMap<ClubDto,Club>().ReverseMap();
        CreateMap<ClubInvitationDto, ClubInvitation>().ReverseMap();
        CreateMap<ClubJoinRequestDto, ClubJoinRequest>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();


        CreateMap<AccountDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Role)))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive)).ReverseMap();

        CreateMap<ProfileMessageDto, ProfileMessage>()
            .ForMember(dest => dest.Resource, opt => opt.MapFrom(src =>
                new Resource(
                    (Resource.ResourceType)Enum.Parse(typeof(Resource.ResourceType), src.Resource.Type.ToString()),
                    src.Resource.EntityId
                )
            ));
        CreateMap<ProfileMessage, ProfileMessageDto>()
            .ForMember(dest => dest.Resource, opt => opt.MapFrom(src =>
                new Resource(
                    src.Resource.Type,
                    src.Resource.EntityId
                )
            ));

        CreateMap<Resource, ResourceDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type)) 
            .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId));
        CreateMap<ResourceDto, Resource>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type)) 
            .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId));
    }
}