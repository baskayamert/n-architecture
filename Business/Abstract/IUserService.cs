using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs.PatientDtos;
using Entities.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(User user);
        void Update(User user);
        User GetByUsername(string username);
        User GetById(int userId);
    }
}
