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
        string[] noms = [];
        string[] prenoms = [];
        prenoms = ["Jean", "Paul", "Jacques", "Marie", "Pierre", "Luc", "Lucie", "Julie", "Julien", "Juliette", "Ansgar", "Clement", "Clementine", "Clemence", "Clemens", "Clemmie", "Cleo", "Cleopatra", "Clerc", "Cletus", "Cleve", "Cleveland", "Clio", "Clive", "Clo", "Clotilde", "Clovis", "Cloyd", "Clyve"];
        noms = ["Dupont", "Durand", "Duchemin", "Duchesne", "Dufour", "Dumas", "Dumont", "Dumoulin", "Dumond", "Dumaine", "Dumais", "Dulac", "Dulude", "Dulac", "Dulude", "Paro", "Wetzel", "Alea", "Mathos", "Meli", "Saintogu"];

        // Add dummy users
        var users = new List<User>();
        for (int i = 1; i <= 18; i++)
        {
            users.Add(new User($"u{i}", $"p{i}", $"{prenoms[i - 1]}", $"{noms[i - 1]}")
            {
                Id = i
            });
        }
        var mod = new User("modo1", "p1", "Ansgarus", "Bretzel")
        {
            Id = 19
        };
        users.Add(mod);

        var games = new List<Game>
        {
            new("ENSC 2024")
        };
        var teams = new List<Equipe>
        {
            new(1, "Rouges","CC0000"){Id=1},
            new(1, "Oranges","E69138"){Id=2},
            new(1, "Jaunes","F1C232"){Id=3},
            new(1, "Verts","6AA84F"){Id=4},
            new(1, "Bleus","3D85C6"){Id=5},
        };
        var modo1 = new Moderate(1, 19);
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
        context.Moderators.Add(modo1);


        // Commit changes into DB
        context.SaveChanges();
    }
}