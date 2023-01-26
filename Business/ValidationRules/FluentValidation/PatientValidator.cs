using Entities.Concrete;
using Entities.DTOs.PatientDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PatientValidator
    {
        public class PatientRegisterValidator : AbstractValidator<PatientRegisterDto>
        {
            public PatientRegisterValidator()
            {
                RuleFor(p => p.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(80);

                RuleFor(p => p.NationalityNumber).NotNull().NotEmpty().Length(11);

                RuleFor(p => p.HospitalId).NotNull().NotEmpty();
            }
        }
        public class PatientUpdateValidator : AbstractValidator<PatientUpdateDto>
        {
            public PatientUpdateValidator()
            {
                RuleFor(p => p.Id).NotNull().NotEmpty();
                RuleFor(p => p.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(80);

                RuleFor(p => p.NationalityNumber).NotNull().NotEmpty().MaximumLength(20);

                RuleFor(p => p.HospitalId).NotNull().NotEmpty();
            }
        }
        public class PatientDeletionValidator : AbstractValidator<PatientDeletionDto>
        {
            public PatientDeletionValidator()
            {
                RuleFor(p => p.Id).NotNull().NotEmpty();
            }
        }

    }
}
