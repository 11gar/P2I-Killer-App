namespace ApiProjet.Data;

public static class SeedData
{
    // Test data for part 1 and 2
    public static void Init()
    {
        using var context = new ApiContext();
        // context.Users.RemoveRange(context.Users);
        // context.Games.RemoveRange(context.Games);
        // context.Kills.RemoveRange(context.Kills);
        // context.UsersInGames.RemoveRange(context.UsersInGames);
        // context.Moderators.RemoveRange(context.Moderators);
        // context.Equipes.RemoveRange(context.Equipes);
        // context.SaveChanges();
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
            new(1,"ENSC 2024"),
            new(2,"ENSC 2025"),
        };
        var teams = new List<Equipe>
        {
            new(1, "Rouges","CC0000"){Id=1},
            new(1, "Oranges","E69138"){Id=2},
            new(1, "Jaunes","F1C232"){Id=3},
            new(1, "Verts","6AA84F"){Id=4},
            new(1, "Bleus","3D85C6"){Id=5},
            new(2, "Rouges","CC0000"){Id=6},
            new(2, "Oranges","E69138"){Id=7},
            new(2, "Jaunes","F1C232"){Id=8},
            new(2, "Verts","6AA84F"){Id=9},
            new(2, "Bleus","3D85C6"){Id=10},
        };
        var modo1 = new Moderate(1, 19);
        var modo2 = new Moderate(2, 19);
        var usersInGames = new List<UserInGame>();
        Random rng = new Random();
        foreach (var user in users)
        {
            if (user.Id != 19)
            {
                usersInGames.Add(new UserInGame
                {
                    IdUser = user.Id,
                    IdGame = 1,
                    Alive = true,
                    Famille = rng.Next(1, 6),
                    Kills = 0
                });
                usersInGames.Add(new UserInGame
                {
                    IdUser = user.Id,
                    IdGame = 2,
                    Alive = true,
                    Famille = rng.Next(6, 11),
                    Kills = 0
                });
            }
        }
        context.Users.AddRange(users);
        context.Games.AddRange(games);
        context.Equipes.AddRange(teams);
        context.UsersInGames.AddRange(usersInGames);
        context.Moderators.Add(modo1);
        context.Moderators.Add(modo2);


        // Commit changes into DB
        context.SaveChanges();
    }
}