using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ActionFilters.Validation
{
    public class ValidationFilterAttribute<TEntity,TValidator> : IActionFilter where TValidator : IValidator, new()
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is TEntity);
            ValidationResult result = ValidationTool.Validate(new TValidator(), param.Value);

            if (!result.IsValid)
            {
                context.HttpContext.Response.StatusCode = 400;

                context.Result = new JsonResult(result.Errors);

            }
        }
    }
}
