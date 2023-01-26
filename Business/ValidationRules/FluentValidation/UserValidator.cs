using Entities.Concrete;
using Entities.DTOs.HospitalDtos;
using Entities.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator
    {
        public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
        {
            public UserRegisterValidator()
            {
                RuleFor(u => u.Username).NotEmpty().MaximumLength(15);
                RuleFor(u => u.Password).NotEmpty().MaximumLength(15);
                RuleFor(u => u.Email).NotEmpty().MaximumLength(50);
                RuleFor(u => u.UserType).NotEmpty();
            }
        }
        public class UserLoginValidator : AbstractValidator<UserLoginDto>
        {
            public UserLoginValidator()
            {
                RuleFor(u => u.Username).NotNull().NotEmpty().MaximumLength(15);
                RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(15);
            }
        }
        public class UserDeletionValidator : AbstractValidator<UserDeletionDto>
        {
            public UserDeletionValidator()
            {
                RuleFor(u => u.Id).NotEmpty();
            }
        }
        public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
        {
            public UserUpdateValidator()
            {
                RuleFor(u => u.Username).NotEmpty();
                RuleFor(u => u.Password).NotEmpty();
                RuleFor(u => u.Email).NotEmpty();
                RuleFor(u => u.UserType).NotEmpty();

            }
        }

    }

}
