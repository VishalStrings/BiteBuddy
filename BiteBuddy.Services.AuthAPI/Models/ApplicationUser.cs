using Microsoft.AspNetCore.Identity;

namespace BiteBuddy.Services.AuthAPI.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
