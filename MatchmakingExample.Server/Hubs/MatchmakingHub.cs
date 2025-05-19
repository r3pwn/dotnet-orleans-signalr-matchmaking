using MatchmakingExample.Server.Grains.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace MatchmakingExample.Server.Hubs;

public class MatchmakingHub(IGrainFactory grainFactory) : Hub
{
    public override async Task OnConnectedAsync()
    {
        // add player to the matchmaking queue as soon as they connect
        await QuickMatch();
        await base.OnConnectedAsync();
    }

    public async Task QuickMatch()
    {
        var matchmaking = grainFactory.GetGrain<IMatchmakerGrain>("QuickMatch");
        await matchmaking.JoinQueue(Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var matchmaking = grainFactory.GetGrain<IMatchmakerGrain>("QuickMatch");
        await matchmaking.LeaveQueue(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}
