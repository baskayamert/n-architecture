using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.UserDtos
{
    public class UserRegisterDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        private UserType _userType = UserType.User;
        public UserType UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                _userType = value;
            }
        }
    }
}
