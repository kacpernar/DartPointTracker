using DartPointTracker.Models;

namespace DartPointTracker.Services;

public class GameService : IGameService
{
    public Game? CurrentGame { get; set; }

    public Task InitializeGame(List<Player> players, int gamePoints)
    {
        if(CurrentGame is not null)
            throw new InvalidOperationException("Game already initialized");

        CurrentGame = new Game(players, gamePoints);
        return Task.CompletedTask;
    }

}