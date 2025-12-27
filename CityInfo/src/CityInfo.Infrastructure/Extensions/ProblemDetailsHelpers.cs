using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Infrastructure.Extensions
{
    public static class ProblemDetailsHelpers
    {
        public static void ApplyValidationDefaults(
            HttpContext httpContext,
            ProblemDetails problem)
        {

            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            
            problem.Type = "Model Validation Problem";
            problem.Status = StatusCodes.Status422UnprocessableEntity;
            problem.Title = "One or more validation errors occured.";
            problem.Detail = "See the errors field for details";
        }
    }
}
