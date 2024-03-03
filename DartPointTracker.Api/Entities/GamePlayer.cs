namespace DartPointTracker.Api;

public class GamePlayer
{
    string GameId { get; set; }
    string PlayerId { get; set; }
    int Place { get; set; }
    Game Game { get; set; }
    Player Player { get; set; }
}