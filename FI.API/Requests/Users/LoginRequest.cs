﻿using FluentValidation;

namespace FI.API.Requests.Users
{
    public class LoginRequest
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}
