using DartPointTracker.Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GameDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/Game", ([FromServices] GameDbContext gameDbContext, List<PlayerDto> players) =>
    {
        var playersIds = players.Select(p => p.Id).ToList();
        var playersInGame = gameDbContext.Players.Where(p => playersIds.Contains(p.Id)).ToList();
        if (playersInGame.Count != playersIds.Count)
        {
            return Results.BadRequest("One or more players do not exist");
        }
        var game = new Game();
        game.Players = playersInGame;
        game.CalculateELOs(playersInGame, players);
        gameDbContext.SaveChanges();
        return Results.Ok(game);
    })
    .WithName("Game")
    .WithOpenApi();

app.MapPost("/Player", ([FromServices] GameDbContext gameDbContext,string playerName) =>
    {
        if(gameDbContext.Players.Any(p => p.Name == playerName))
        {
            return Results.BadRequest("Player with this name already exists");
        }
        var player = new Player(playerName);
        gameDbContext.Players.Add(player);
        gameDbContext.SaveChanges();
        return Results.Created($"/Player/{player.Name}", player);
    })
    .WithOpenApi();
app.MapGet("/Players", ([FromServices] GameDbContext gameDbContext) =>
    {
        return Results.Ok(gameDbContext.Players.ToList());
    })
    .WithOpenApi();
app.Run();

