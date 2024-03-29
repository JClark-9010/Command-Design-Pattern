﻿namespace TakeNotesServer.Models
{
        public class User
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string Salt { get; set; }
        }

        public class ReturnUser
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            //public string Role { get; set; }
            public string Token { get; set; }
        }

        public class LoginUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
}
