using DartPointTracker.Api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GameDbContext>();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

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
    .RequireAuthorization()
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
    .RequireAuthorization()
    .WithOpenApi();
app.MapGet("/Players", ([FromServices] GameDbContext gameDbContext) =>
    {
        return Results.Ok(gameDbContext.Players.ToList());
    })
    .RequireAuthorization()
    .WithOpenApi();
app.MapPost("/GeneratePlayers/{count}", ([FromServices] GameDbContext gameDbContext, int count) =>
    {
        if (count <= 0)
        {
            return Results.BadRequest("Count must be greater than zero");
        }

        var random = new Random();
        var players = new List<Player>();

        for (int i = 0; i < count-1; i++)
        {
            string playerName = $"Player_{Guid.NewGuid().ToString().Substring(0, 8)}";

            int eloRankingScore = random.Next(800, 1200);
            var player = new Player(playerName, eloRankingScore);
            players.Add(player);
        }
        if (!gameDbContext.Players.Any(p => p.Name == "Last_Player"))
        {
            var lastPlayer = new Player("Last_Player", 700);
            players.Add(lastPlayer);
        }
        else
        {
            string playerName = $"Player_{Guid.NewGuid().ToString().Substring(0, 8)}";

            int eloRankingScore = random.Next(800, 1200);
            var player = new Player(playerName, eloRankingScore);
            players.Add(player);
        }

        gameDbContext.Players.AddRange(players);
        gameDbContext.SaveChanges();

        return Results.Created("/GeneratePlayers", players);
    })
    .RequireAuthorization()
    .WithOpenApi();

app.MapDelete("/DeletePlayers/{count}", ([FromServices] GameDbContext gameDbContext, int count) =>
    {
        if (count <= 0)
        {
            return Results.BadRequest("Count must be greater than zero");
        }

        var playersToDelete = gameDbContext.Players.OrderBy(p => p.Id).Take(count).ToList();

        if (!playersToDelete.Any())
        {
            return Results.NotFound("No players found to delete");
        }

        gameDbContext.Players.RemoveRange(playersToDelete);
        gameDbContext.SaveChanges();

        return Results.Ok($"Deleted {playersToDelete.Count} players");
    })
    .RequireAuthorization()
    .WithOpenApi();
app.Run();

