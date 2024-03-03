using Microsoft.EntityFrameworkCore;

namespace DartPointTracker.Api;

public class GameDbContext : DbContext
{
    public virtual DbSet<Player> Players { get; set; }

    public string DbPath { get; }

    public GameDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "game.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}