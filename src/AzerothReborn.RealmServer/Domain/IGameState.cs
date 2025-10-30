namespace AzerothReborn.RealmServer.Domain;

internal interface IGameState
{
    void Transaction(Action<Game> update);
}
