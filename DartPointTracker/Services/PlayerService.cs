using System.Net.Http.Json;
using DartPointTracker.Models;
namespace DartPointTracker;
public class PlayerService(HttpClient httpClient) : IPlayerService{
    public List<Player> Players { get; set; } = [];

    public async Task<string?> CreatePlayer(string? name)
    {
        try {
            if (name == null) {
                return "Name cannot be empty"; }
            var response = await httpClient.PostAsync("/Player?playerName=" + name, null);
            if (response.IsSuccessStatusCode) {
                await GetPlayers();
                return null; }
            var errorMessage = await response.Content.ReadAsStringAsync();
            return errorMessage; }
        catch (Exception) {
            return "You are offline. Please connect to the internet to create a new player."; } }
    public async Task GetPlayers() {
        try {
            Players = await httpClient.GetFromJsonAsync<List<Player>>("/Players") ?? []; }
        catch (HttpRequestException ex) {
            Console.WriteLine($"Failed to fetch players from API: {ex.Message}");
            Players = []; }
        catch (Exception ex) {
            Console.WriteLine($"Failed to fetch players from API: {ex.Message}");
            Players = []; } }

    public async Task<(bool, string)> SendGame(Game game) {
        try {
            var playerList = game.Ranking
                .Select((player, index) => new { Id = player.Id, Place = index + 1 }).ToList();
            var response = await httpClient.PostAsJsonAsync("/Game", playerList);
            if (response.IsSuccessStatusCode) {
                game.GameSaved = true;
                return (true, "The game was saved successfully, check the ranking to see the updated leaderboard."); }
            game.GameSaved = false;
            return (false, "Failed to save game, please try again."); }
        catch (HttpRequestException ex) {
            game.GameSaved = true;
            return (false, "Seems like you are offline. Your game will be saved once you are back online."); } } }
