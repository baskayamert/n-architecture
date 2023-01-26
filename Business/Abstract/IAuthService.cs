using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs.HospitalDtos;
using Entities.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto user, string password);
        IDataResult<User> Login(UserLoginDto user);
        IResult UserExist(string username);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<RefreshToken> CreateRefreshToken(User user);

    }
}
