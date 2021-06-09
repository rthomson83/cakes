using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cakes.Api.Models
{
    public class ValidationErrorResult : ObjectResult
    {
        public ValidationErrorResult(ModelStateDictionary modelState) 
            : base(new ValidationErrors(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}