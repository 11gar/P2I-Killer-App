namespace ApiProjet.Data;

public static class SeedData
{
    // Test data for part 1 and 2
    public static void Init()
    {
        using var context = new ApiContext();
        // Look for existing content
        if (context.Users.Any())
        {
            Console.WriteLine($"DB already filled");
            return;   // DB already filled
        }
        System.Console.WriteLine($"DB empty, filling it with test data");

        // Add dummy users
        var users = new List<User>
        {
            new("user1", "password1","pre","nom"),
            new("user2", "password2","pre","nom"),
            new("user3", "password3","pre","nom"),
        };
        var games = new List<Game>
        {
            new("game1", "game1"),
            new("game1", "game2"),
            new("game1", "game3"),
        };
        var usersInGames = new List<UserInGame>
        {
            new UserInGame{
                Id=1,
                IdUser = 1,
                IdGame = 1,
                IdCible = 2,
                Alive = true,
                Kills=0
            },
            new UserInGame{
                Id=2,
                IdUser = 2,
                IdGame = 1,
                IdCible = 3,
                Alive = true,
                Kills=0
            },
            new UserInGame{
                Id=3,
                IdUser = 3,
                IdGame = 1,
                IdCible = 1,
                Alive = true,
                Kills=0
            },

        };
        context.Users.AddRange(users);
        context.Games.AddRange(games);
        context.UsersInGames.AddRange(usersInGames);


        // Commit changes into DB
        context.SaveChanges();
    }
}