namespace Mangos.Configuration;

public sealed class CharacterConfiguration
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}