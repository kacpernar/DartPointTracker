namespace DartPointTracker.Models;

public class Player(string name)
{
    public string Name { get; set; } = name;
    public int Score { get; set; }
    public List<Throw> DartThrows { get; set; } = [];
    public int CurrenThrowNumber = 1;

    public int[] CurrentRoundThrows = new int[3];

    public bool IsFinished => Score == 0;


    public void AddDartThrow(int points)
    {
        CurrentRoundThrows[CurrenThrowNumber % 3 - 1] = points;
        DartThrows.Add(new Throw(CurrenThrowNumber, points));
        CurrenThrowNumber++;
        Score -= points;
    }
    public void RemoveLastDartThrow()
    {
        if (CurrenThrowNumber == 1)
            return;

        var lastThrow = DartThrows[CurrenThrowNumber - 1];
        DartThrows.Remove(lastThrow);
        Score += lastThrow.Score;
        CurrenThrowNumber--;
    }

}

