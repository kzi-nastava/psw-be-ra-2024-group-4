using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.TourAuthoring
{
    public interface IRegistrationRequestService
    {
        Result<RegistrationRequestDto> Create(RegistrationRequestDto registrationRequestDto);
        Result<RegistrationRequestDto> Update(RegistrationRequestDto registrationRequestDto);
    }
}
