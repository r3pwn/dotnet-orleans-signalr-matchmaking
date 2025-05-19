using MatchmakingExample.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.UseSignalR();
    siloBuilder.RegisterHub<MatchmakingHub>();
});

builder.Services.AddSignalR().AddOrleans();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<MatchmakingHub>("/matchmaking");

app.Run();
