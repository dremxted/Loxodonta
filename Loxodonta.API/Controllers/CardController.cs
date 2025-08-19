using Loxodonta.Application.Cards;
using Loxodonta.Application.Contracts;
using Loxodonta.Common;
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
        return result.Match<CardDto,IActionResult>(
            card => Created("",card),
            BadRequest);
    }
}
