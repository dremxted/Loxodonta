namespace Loxodonta.Domain.Cards;

public class Card
{
    private List<Feature> _features = new();
    public Guid Id { get; private set; } = Guid.NewGuid();
    public IReadOnlyCollection<Feature> Features => _features;

    public void AddFeature(string name, string value, int order)
    {
        _features.Add(new Feature(this, name, value, order));
    }

    public void ClearFeatures()
    {
        _features.Clear();
    }
}
