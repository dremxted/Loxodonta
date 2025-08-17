using Loxodonta.Application.Cards;
using Loxodonta.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Loxodonta.API.Controllers;

[ApiController]
[Route("api/cards")]
public class CardController(ICardService cardService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCardDto createCardDto)
    {
        var result = await cardService.CreateAsync(createCardDto);
        return result.Match<IActionResult>((card) => Created("",result.Value),BadRequest);
    }
}