using MatchmakingExample.Server.Grains.Interfaces;
using MatchmakingExample.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MatchmakingExample.Server.Grains;

public class MatchmakerGrain(IHubContext<MatchmakingHub> matchmakingHub) : Grain, IMatchmakerGrain
{
    private readonly Queue<string> _queue = new();

    public async Task JoinQueue(string connectionId)
    {
        _queue.Enqueue(connectionId);

        if (_queue.Count >= 3)
        {
            var playerList = _queue.ToList();
            _queue.Clear();

            var gameId = Guid.NewGuid().ToString();

            await GrainFactory.GetGrain<IGameSessionGrain>(gameId).Initialize(playerList);
            
            await matchmakingHub.Clients.Clients(playerList).SendAsync("match:found", gameId);
        }
    }

    public Task LeaveQueue(string connectionId)
    {
        // Simple removal
        var tempList = _queue.ToList();
        tempList.Remove(connectionId);
        _queue.Clear();
        foreach (var id in tempList)
        {
            _queue.Enqueue(id);
        }
        return Task.CompletedTask;
    }
}
