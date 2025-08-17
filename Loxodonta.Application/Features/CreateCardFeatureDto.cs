namespace Loxodonta.Application.Features;

public class CreateCardFeatureDto
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int Order { get; set; }
}
