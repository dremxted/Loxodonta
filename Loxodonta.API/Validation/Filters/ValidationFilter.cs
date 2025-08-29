using FluentValidation;
using FluentValidation.Results;
using Loxodonta.API.Validation.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Loxodonta.API.Validation.Filters;

public sealed class ValidationFilter<TModel>(
    IValidator<TModel> validator, 
    IValidationObjectResultFactory objectResultFactory) 
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        object model = context.ActionArguments
            .FirstOrDefault(arg => arg.Value is TModel)
            .Value!;

        var validationResult = await validator.ValidateAsync((TModel)model);

        if (!validationResult.IsValid)
        {
            context.Result = objectResultFactory
                .Create(ToModelStateDictionary(validationResult)); 
            return;
        }

        await next();
    }

    private ModelStateDictionary ToModelStateDictionary(ValidationResult validationResult)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in validationResult.Errors)
        {
            modelStateDictionary.AddModelError(
                error.PropertyName,
                error.ErrorMessage);
        }
        return modelStateDictionary;
    }
}