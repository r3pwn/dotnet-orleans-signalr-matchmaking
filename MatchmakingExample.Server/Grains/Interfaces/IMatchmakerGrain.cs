namespace MatchmakingExample.Server.Grains.Interfaces;

[Alias("MatchmakingExample.Server.Grains.Interfaces.IMatchmakerGrain")]
public interface IMatchmakerGrain : IGrainWithStringKey
{
    [Alias("JoinQueue")]
    Task JoinQueue(string connectionId);
    [Alias("LeaveQueue")]
    Task LeaveQueue(string connectionId);
}
