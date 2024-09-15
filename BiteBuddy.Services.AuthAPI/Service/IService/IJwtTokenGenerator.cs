using BiteBuddy.Services.AuthAPI.Models;

namespace BiteBuddy.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {

        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
