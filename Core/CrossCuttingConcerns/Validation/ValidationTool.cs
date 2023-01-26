﻿using Core.Utilities.Results;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static ValidationResult Validate(IValidator validator, object entity) 
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            return result;
        }
    }
}
