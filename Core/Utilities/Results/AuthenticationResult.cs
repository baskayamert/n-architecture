using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class AuthenticationResult : Result
    {
        public string? AccessToken { get; }
        public string? RefreshToken { get; }

        public AuthenticationResult(bool success, string message, string accessToken, string refreshToken) : base(success, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public AuthenticationResult(bool success, string accessToken, string refreshToken) : base(success)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
