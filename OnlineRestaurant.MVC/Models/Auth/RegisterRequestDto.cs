﻿using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.MVC.Models.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Name { get; set; }
    }
}
