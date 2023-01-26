using Core.Entities.Concrete;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private AccessTokenOptions _accessTokenOptions;
        private RefreshTokenOptions _refreshTokenOptions;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _accessTokenOptions = Configuration.GetSection("TokenOptions").Get<AccessTokenOptions>();
            _refreshTokenOptions = Configuration.GetSection("TokenOptions").Get<RefreshTokenOptions>();

        }


        public AccessToken CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_accessTokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_accessTokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_accessTokenOptions, user, signingCredentials, accessTokenExpiration);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, DateTime tokenExpiration)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: tokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("UserType", user.UserType.ToString()));
            claims.Add(new Claim("UserId", user.Id.ToString()));

            return claims;
        }

        public RefreshToken CreateRefreshToken(User user)
        {
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_refreshTokenOptions.RefreshTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_refreshTokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_refreshTokenOptions, user, signingCredentials, refreshTokenExpiration);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new RefreshToken
            {
                Token = token,
                CreationDate = DateTime.Now,
                ExpirationDate = refreshTokenExpiration,
                UserId = user.Id,
                IsExpired = false
            };

        }

    }
}
