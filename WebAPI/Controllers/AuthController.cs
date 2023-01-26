using Business.Abstract;
using Business.ActionFilters.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static Business.ValidationRules.FluentValidation.UserValidator;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IRefreshTokenService _refreshTokenService;
        private IUserService _userService;

        public AuthController(IAuthService authService, IRefreshTokenService refreshTokenService, IUserService userService)
        {
            _authService = authService;
            _refreshTokenService = refreshTokenService;
            _userService = userService;
        }
        
        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken(string refreshToken)
        {
            //Current Access Token
            var accessToken = HttpContext.Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value;
            accessToken = accessToken.ToString().Split("Bearer ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(accessToken);
            var accessTokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var accessTokenTicks = long.Parse(accessTokenExp);

            //Current Refresh Token
            var refreshTokenObj = _refreshTokenService.GetByToken(refreshToken);
            User userData = _userService.GetById(refreshTokenObj.UserId);


            if(refreshTokenObj.ExpirationDate.Ticks >= DateTime.Now.Ticks && accessTokenTicks >= DateTime.Now.Ticks)
            {
                HttpContext.Response.StatusCode = 403;
                return new JsonResult("Forbidden area!");
            }
            var newAccessToken = _authService.CreateAccessToken(userData);

            if (refreshTokenObj is null)
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (refreshTokenObj.ExpirationDate.Ticks >= DateTime.Now.Ticks)
            {
                return Unauthorized("Refresh token expired.");
            }
            else if (DateTime.Now.Ticks >= (refreshTokenObj.ExpirationDate.Ticks / 95) * 100)
            {
                
                var newRefreshToken = _authService.CreateRefreshToken(userData);

                refreshTokenObj.IsExpired = true;
                _refreshTokenService.Update(refreshTokenObj);

                AuthenticationResult result2 = new AuthenticationResult(newAccessToken.Success && newRefreshToken.Success ? true : false, "Access token and refresh token were refreshed.", newAccessToken.Data.Token, newRefreshToken.Data.Token);
                return Ok(result2);
            }
            AuthenticationResult result = new AuthenticationResult(newAccessToken.Success && true, "Access token was refreshed.", newAccessToken.Data.Token, refreshToken);

            return Ok(result);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute<UserLoginDto, UserLoginValidator>))]
        public IActionResult Login(UserLoginDto user)
        {
            var userToLogin = _authService.Login(user);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var accessToken = _authService.CreateAccessToken(userToLogin.Data);

            var refreshToken = _authService.CreateRefreshToken(userToLogin.Data);

            AuthenticationResult result = new AuthenticationResult(accessToken.Success && refreshToken.Success ? true : false, "Authentication successful", accessToken.Data.Token, refreshToken.Data.Token);


            if (result.Success)
            {
                return Ok(result);
            }
  
            return BadRequest(result);
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute<UserRegisterDto, UserRegisterValidator>))]

        public IActionResult Register(UserRegisterDto user)
        {
            var userExists = _authService.UserExist(user.Username);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(user, user.Password);
            if (registerResult.Success)
            {
                return Ok(registerResult.Data);
            }

            return BadRequest(registerResult.Message);
        }

    }
}
