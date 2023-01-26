using Entities.Concrete;
using Entities.DTOs.HospitalDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class HospitalValidator
    {
        public class HospitalRegisterValidator : AbstractValidator<HospitalRegisterDto>
        {
            public HospitalRegisterValidator()
            {
                RuleFor(h => h.Name).NotEmpty().MaximumLength(30);
                RuleFor(h => h.Lat).NotEmpty().MaximumLength(15);
                RuleFor(h => h.Long).NotEmpty().MaximumLength(15);
                RuleFor(h => h.Address).NotEmpty().MaximumLength(80);
            }
        }
        public class HospitalUpdateValidator : AbstractValidator<HospitalUpdateDto>
        {
            public HospitalUpdateValidator()
            {
                RuleFor(h => h.Id).NotNull().NotEmpty();
                RuleFor(h => h.Name).MaximumLength(30);
                RuleFor(h => h.Lat).MaximumLength(15);
                RuleFor(h => h.Long).MaximumLength(15);
                RuleFor(h => h.Address).MaximumLength(80);
            }
        }
        public class HospitalDeletionValidator : AbstractValidator<HospitalDeletionDto>
        {
            public HospitalDeletionValidator()
            {
                RuleFor(h => h.Id).NotNull().NotEmpty();
            }
        }

    }

}
