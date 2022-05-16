﻿using System;
using System.Text.Json;

namespace FI.Infrastructure.Models.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(ErrorCode statusCode, string message) :
            base(JsonSerializer.Serialize(new
            {
                StatusCode = (uint)statusCode,
                Message = message,
            }))
        {
        }
    }
}
