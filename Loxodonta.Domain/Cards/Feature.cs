namespace Loxodonta.Domain.Cards;

public class Feature
{
    public Guid Id { get; private set; }
    public int Order { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    public Guid? CardId { get; private set; }
    public Card? Card { get; private set; }

    private Feature() { }
    internal Feature(Card card, string name, string value, int order)
    {
        Card = card;
        CardId = card.Id;
        Name = name;
        Value = value;
        Order = order;
    }
}
