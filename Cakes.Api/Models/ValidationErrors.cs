using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cakes.Api.Models
{
    public class ValidationErrors
    {
        public string Message { get; } 

        public List<ValidationErrorDetail> Errors { get; }

        public ValidationErrors(ModelStateDictionary modelState)
        {
            Message = "Validation Errors";
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationErrorDetail(char.ToLowerInvariant(key[0]) + key.Substring(1), x.ErrorMessage)))
                .ToList();
        }
    }
}