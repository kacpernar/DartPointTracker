namespace DartPointTracker.Models;
public class Game(List<Player> players, int gamePoints) {
    private string GameId { get; set; } = Guid.NewGuid().ToString();
    public List<Player> Players { get; set; } = players.Select(player => {
        player.Score = gamePoints;
        return player;
    }).ToList();
    public List<Player> Ranking { get; set; } = [];
    public Player CurrentPlayer { get; set; } = players[0];
    public bool IsFinished { get; set; }
    public bool GameSaved { get; set; }
    private int TotalThrows { get; set; } = 0;
    public void AddDartThrow(int points) {
        TotalThrows++;
        var (validThrow, currentRoundThrowNumber) = CurrentPlayer.AddDartThrow(points);
        if (CurrentPlayer.Won) {
            Ranking.Add(CurrentPlayer);
            CurrentPlayer.ThrowNumberInGameAtWin = TotalThrows;
            if (Players.Count(player => player.Won) == Players.Count - 1) {
                IsFinished = true;
                Ranking.Add(Players.First(player => !player.Won));
                return; }
            NextPlayer(); }
        else if (!validThrow || currentRoundThrowNumber == 0) {
            NextPlayer(); } }
    public void RemoveLastDartThrow() {
        if (TotalThrows > 0) {
            TotalThrows--; }
        else {
            return; }
        if (CurrentPlayer.CurrentRoundThrowNumber == 0) {
            CurrentPlayer.ReloadPreviousRound();
            PreviousPlayer(); }
        while (CurrentPlayer.Won) {
            if (CurrentPlayer.ThrowNumberInGameAtWin == TotalThrows + 1) {
                CurrentPlayer.Won = false;
                break; }
            PreviousPlayer(); }
        CurrentPlayer.RemoveLastDartThrow(); }
    private void PreviousPlayer() {
        var index = Players.IndexOf(CurrentPlayer);
        CurrentPlayer = index == 0 ? Players[^1] : Players[index - 1]; }
    private void NextPlayer() {
        do {
            var index = Players.IndexOf(CurrentPlayer);
            CurrentPlayer = index == Players.Count - 1 ? Players[0] : Players[index + 1];
        } while (CurrentPlayer.Won);
        CurrentPlayer.StartNewRound(); } }