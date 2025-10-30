namespace AzerothReborn.RealmServer.Domain;

internal sealed class GameState : IGameState
{
    private readonly Game world = new();

    public void Transaction(Action<Game> transaction)
    {
        lock (world)
        {
            transaction(world);
        }
    }
}
