using CollegeStudentXtramile.Server.Api.Resources.Responses;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CollegeStudentXtramile.Server.Api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = new Dictionary<string, List<string>>();

            foreach (var key in context.ModelState.Keys)
            {
                var errorMessages = context.ModelState[key].Errors.Select(e => e.ErrorMessage).ToList();
                if (errorMessages.Any())
                {
                    errors.Add(key, errorMessages);
                }
            }

            var errorResponse = new ErrorResponseJSON(
                TraceId: context.HttpContext.TraceIdentifier,
                Title: "Validation Error",
                Errors: errors
            );

            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
