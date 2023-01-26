using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        IRefreshTokenService _refreshTokenService;
        ITokenHelper _tokenHelper;
        IMapper _mapper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IRefreshTokenService refreshTokenService, IMapper mapper)
        {
            _userService = userService;
            _refreshTokenService = refreshTokenService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, "Token was created.");
        }

        public IDataResult<RefreshToken> CreateRefreshToken(User user)
        {
            var refreshToken = _tokenHelper.CreateRefreshToken(user);


            _refreshTokenService.Add(refreshToken);

            return new SuccessDataResult<RefreshToken>(refreshToken, "Refresh token was created.");
        }

        public IDataResult<User> Login(UserLoginDto user)
        {
            var userToCheck = _userService.GetByUsername(user.Username);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(null, "The user cannot be found.");
            }

            if (!HashingHelper.VerifyPasswordHash(user.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(null, "The password does not match.");
            }

            return new SuccessDataResult<User>(userToCheck, "The user successfully logged in.");
        }

        public IDataResult<User> Register(UserRegisterDto user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var registeredUser = _mapper.Map<User>(user);
            registeredUser.PasswordHash = passwordHash;
            registeredUser.PasswordSalt = passwordSalt;

            _userService.Add(registeredUser);
            return new SuccessDataResult<User>(registeredUser, "The user has been registered.");
        }

        public IResult UserExist(string username)
        {
            if (_userService.GetByUsername(username) != null)
            {
                return new ErrorResult("The user exists.");
            }
            return new SuccessResult();
        }
    }
}
