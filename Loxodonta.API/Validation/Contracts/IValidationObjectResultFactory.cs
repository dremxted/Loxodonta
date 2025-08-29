using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Loxodonta.API.Validation.Contracts;

public interface IValidationObjectResultFactory
{
    IActionResult Create(ModelStateDictionary modelStateDictionary);
}
