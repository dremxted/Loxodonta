using Loxodonta.Common;

namespace Loxodonta.Domain.Cards;

public static class CardErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound(
        "Cards.Card",
        $"The card with id '{id}' was not found.");
    public static Error Failure(Guid id) =>
        Error.Failure("Cards.Card", $"Operation failed with card '{id}'");

    public static Error Validation(params object[] extensions) =>
        Error.Validation("Cards.Card", "Validation error has occured.", extensions);
}