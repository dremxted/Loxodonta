using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Loxodonta.API.Validation.ModelStateResultFactories;

public class ValidationBadRequestFactory : IValidationObjectResultFactory
{
    public IActionResult Create(ModelStateDictionary modelStateDictionary)
    {
        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest
        };

        return new BadRequestObjectResult(problemDetails);
    }
}