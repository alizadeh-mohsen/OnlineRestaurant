﻿namespace OnlineRestaurant.Services.AuthAPI.Models.Dto
{
    public class RegisterRequestDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Name { get; set; }
    }
}
