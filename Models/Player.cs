namespace DartPointTracker.Models;

public class Player(string name)
{
    public string Name { get; set; } = name;
    public int Score { get; set; }
    public List<Throw> DartThrows { get; set; } = [];
    public int CurrentThrowNumber => DartThrows.Count;

    public int[] CurrentRoundThrows = [0, 0, 0];
    public bool Won => Score == 0;


    public void StartNewRound()
    {
        CurrentRoundThrows = [0, 0, 0];
    }
    public void AddDartThrow(int points)
    {
        CurrentRoundThrows[CurrentThrowNumber%3] = points;
        DartThrows.Add(new Throw(CurrentThrowNumber, points));
        Score -= points;
    }
    public void RemoveLastDartThrow()
    {
        if (CurrentThrowNumber == 0)
            return;

        var lastThrow = DartThrows.Last();
        DartThrows.Remove(lastThrow);
        CurrentRoundThrows[CurrentThrowNumber%3] = 0;
        Score += lastThrow.Score;

    }

    public void ReloadPreviousRound()
    {
        if(CurrentThrowNumber < 3)
            return;
        CurrentRoundThrows[0] = DartThrows[^3].Score;
        CurrentRoundThrows[1] = DartThrows[^2].Score;
        CurrentRoundThrows[2] = DartThrows[^1].Score;
    }

}

