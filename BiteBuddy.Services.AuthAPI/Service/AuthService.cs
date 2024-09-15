using BiteBuddy.Services.AuthAPI.Data;
using BiteBuddy.Services.AuthAPI.Models;
using BiteBuddy.Services.AuthAPI.Models.Dto;
using BiteBuddy.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlTypes;

namespace BiteBuddy.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _JwtTokenGenerator;
        public AuthService(ApplicationDBContext dBContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator JwtTokenGenerator)
        {
            _dbContext = dBContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _JwtTokenGenerator = JwtTokenGenerator;
        }

        public async Task<bool>Assignrole(string email, string roleName)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(x=>x.Email.ToLower() == email.ToLower());
            if (user != null) {

                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult()) 
                {
                    // create role if doesnot exists
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto>Login(LoginRequestDto loginRequestDto)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.Username.ToLower());
            bool isvalid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if(user == null || isvalid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            // If User was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _JwtTokenGenerator.GenerateToken(user,roles);

            UserDto userDto = new()
            {
              ID=user.Id, Email = user.Email,Name = user.Name, PhoneNumber= user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string>Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    var userToReturn = _dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName == registrationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        ID = userToReturn.Id,
                        Email = userToReturn.Email,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";
                    
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;

                }
            }
            catch (Exception ex)
            {

            }
            //return new UserDto();
            return "Error Encountered";
        }
    }
}
