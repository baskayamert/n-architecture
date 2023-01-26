using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.PatientDtos;
using Entities.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IRefreshTokenService _refreshTokenService;
        IMapper _mapper;
        public UserManager(IUserDal userDal, IRefreshTokenService refreshTokenService, IMapper mapper)
        {
            _userDal = userDal;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetById(int userId)
        {
            return _userDal.Get(u => u.Id== userId);
        }

        public User GetByUsername(string username)
        {
            var user = _userDal.Get(u => u.Username == username);
            if(user != null)
            {
                user.RefreshTokens = _refreshTokenService.GetByUserId(user.Id);
            }
            
            return user;
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
        
    }
}
