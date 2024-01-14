namespace DartPointTracker.Models;

public class Game
{
    public Game(List<Player> players, int gamePoints)
    {
        GamePoints = gamePoints;
        foreach (var player in players)
        {
            player.Score = gamePoints;
        }

        Players = players;
        CurrentPlayer = players[0];
    }

    private int GamePoints { get; set; }
    public List<Player> Players { get; set; }
    public Player CurrentPlayer { get; set; }

}