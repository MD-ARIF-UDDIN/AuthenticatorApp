﻿using System.ComponentModel.DataAnnotations;

namespace AuthticatorApp.Models.DTO
{
	public class RegistrationModel
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain 1 Uppercase, 1 lowercase, 1 special character, and 1 digit")]
        public string Password { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public string? Role { get; set; }
    }
}
