namespace DartPointTracker.Api;

public class Player(string name, int eloRankingScore = 1000)
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = name;
    public int EloRankingScore { get; set; } = eloRankingScore;
}