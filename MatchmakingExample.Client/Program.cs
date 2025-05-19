using Microsoft.AspNetCore.SignalR.Client;

var matchFound = false;

// Initialize SignalR connection
var hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:44309/matchmaking")
    .WithAutomaticReconnect()
    .Build();

// Listen for "match:found" messages
hubConnection.On<string>("match:found", (message) =>
{
    Console.WriteLine($"Match found: {message}");
    matchFound = true;
});

// Start the connection
await hubConnection.StartAsync();

while (!matchFound)
{
    Console.WriteLine($"Waiting for a match...");
    Thread.Sleep(1000);
}
Console.ReadLine();
await hubConnection.StopAsync();