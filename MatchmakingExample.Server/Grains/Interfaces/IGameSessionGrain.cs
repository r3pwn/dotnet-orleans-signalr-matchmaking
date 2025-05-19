namespace MatchmakingExample.Server.Grains.Interfaces;

[Alias("MatchmakingExample.Server.Grains.Interfaces.IGameSessionGrain")]
public interface IGameSessionGrain : IGrainWithStringKey
{
    [Alias("Initialize")]
    Task Initialize(List<string> players);
}