﻿using System.ComponentModel.DataAnnotations;

namespace STINProject_API.Services.PersistenceService.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }

        // TODO hash password
        [Required]
        public string Password { get; set; }

        // TODO verify if email address is valid
        [Required]
        public string Email { get; set; }

        public IEnumerable<Account> Accounts { get; set; }

        public User(string username, string password, string email)
        {
            UserId = new Guid();
            Username = username;
            Password = password;
            Email = email;
        }

        public User() { }
    }
}
