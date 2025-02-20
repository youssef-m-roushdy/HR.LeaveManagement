using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class ValidatorException : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidatorException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}