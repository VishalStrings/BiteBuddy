﻿namespace BiteBuddy.Services.AuthAPI.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
