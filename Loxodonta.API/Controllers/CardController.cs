using Loxodonta.API.Validation.Filters;
using Loxodonta.Application.Cards;
using Loxodonta.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Loxodonta.API.Controllers;

[ApiController]
[Route("api/cards")]
public class CardController(ICardService cardService) : ControllerBase
{
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<CreateCardDto>))]
    public async Task<IActionResult> CreateAsync(CreateCardDto createCardDto)
    {
        var result = await cardService.CreateAsync(createCardDto);
        return result.Match<IActionResult>(
            (card) =>
            {
                var url = Url.Link(nameof(FindAsync), new { id = card.Id });
                return Created(url, card);
            },
            BadRequest);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await cardService.GetAllAsync();
        var val = result.Value.ToList();

        return result.Match<IActionResult>(Ok, _ => NoContent());
    }

    [HttpGet("{id}", Name = nameof(FindAsync))]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var result = await cardService.FindAsync(id);
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationFilter<UpdateCardDto>))]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCardDto updateCardDto)
    {
        var result = await cardService.UpdateAsync(id, updateCardDto);
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await cardService.DeleteAsync(id);
        return result.Match<IActionResult>(NoContent, NotFound);
    }
}
