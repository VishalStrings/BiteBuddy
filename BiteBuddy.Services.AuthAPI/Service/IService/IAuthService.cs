using BiteBuddy.Services.AuthAPI.Models.Dto;

namespace BiteBuddy.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {

        Task<string>Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> Assignrole(string email, string roleName);
    }
}
