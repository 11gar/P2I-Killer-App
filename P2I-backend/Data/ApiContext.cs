using Microsoft.EntityFrameworkCore;
public class ApiContext : DbContext
{
    public string DbPath { get; private set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<UserInGame> UsersInGames { get; set; }
    public DbSet<Kill> Kills { get; set; }
    public DbSet<Cible> Cibles { get; set; }
    public DbSet<Moderate> Moderators { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public ApiContext()
    {
        // Path to SQLite database file
        DbPath = "BDD/bdd.db";
    }


    // The following configures EF to create a SQLite database file locally
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use SQLite as database
        options.UseSqlite($"Data Source={DbPath}");
        // Optional: log SQL queries to console
        //options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
    }
}
