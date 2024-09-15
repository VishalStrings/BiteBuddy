using BiteBuddy.Services.AuthAPI.Models.Dto;

namespace BiteBuddy.Web.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
