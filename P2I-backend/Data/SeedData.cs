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
            new("user1", "password1","pre","nom"){Id=1},
            new("user2", "password2","pre","nom"){Id=2},
            new("user3", "password3","pre","nom"){Id=3},
        };
        for (int i = 4; i <= 18; i++)
        {
            users.Add(new User($"user{i}", $"password{i}", "pre", "nom")
            {
                Id = i
            });
        }
        var games = new List<Game>
        {
            new("game1"),
            new("game1"),
            new("game1"),
        };
        var teams = new List<Equipe>
        {
            new(1, "team1","CC0000"){Id=1},
            new(1, "team2","E69138"){Id=2},
            new(1, "team3","F1C232"){Id=3},
            new(1, "team4","6AA84F"){Id=4},
            new(1, "team5","3D85C6"){Id=5},
        };
        var usersInGames = new List<UserInGame>();
        Random rng = new Random();
        foreach (var user in users)
        {
            usersInGames.Add(new UserInGame
            {
                IdUser = user.Id,
                IdGame = 1,
                Alive = true,
                Famille = rng.Next(1, 6),
                Kills = 0
            });
        }
        context.Users.AddRange(users);
        context.Games.AddRange(games);
        context.Equipes.AddRange(teams);
        context.UsersInGames.AddRange(usersInGames);


        // Commit changes into DB
        context.SaveChanges();
    }
}