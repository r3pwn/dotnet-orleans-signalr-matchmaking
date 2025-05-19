using MatchmakingExample.Server.Grains.Interfaces;

namespace MatchmakingExample.Server.Grains;

public class GameSessionGrain : Grain, IGameSessionGrain
{
    public Task Initialize(List<string> players)
    {
        // TODO - Store connection IDs or other setup logic
        return Task.CompletedTask;
    }
}