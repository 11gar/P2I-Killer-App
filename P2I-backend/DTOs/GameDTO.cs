
public class GameDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsStarted { get; set; }
    public List<UserInGame> Players { get; set; }
    public List<User> Moderators { get; set; }
    public List<Equipe> Equipes { get; set; }

    public GameDTO(int id, string name, bool isStarted, List<UserInGame> players, List<User> moderators)
    {
        Id = id;
        Name = name;
        IsStarted = isStarted;
        Players = players;
        Moderators = moderators;
    }
    public GameDTO(Game game)
    {
        Id = game.Id;
        Name = game.Name;
        IsStarted = game.IsStarted;
        Players = [];
        Moderators = [];
    }

    public async Task<List<UserInGame>> InitCibles()
    {
        if (Equipes.Count < 2)
        {
            //randomize
            return this.Players;
        }
        else
        {
            Console.WriteLine("players : " + this.Players[0].Id);
            this.Players = await AlgoFormation(this.Players);
            return this.Players;
        }
    }

    public async Task<List<UserInGame>> ChangeCiblesFromCsv(string csvIds)
    {
        if (csvIds == null) return this.Players;
        List<UserInGame> players = new();
        List<string> lines = csvIds.Split(",").ToList();
        Console.WriteLine("lines : " + lines);
        foreach (string line in lines)
        {
            Console.WriteLine("line : " + line);
            if (int.TryParse(line, out _))
            {
                players.Add(this.Players!.Find((p) => p.Id == int.Parse(line))!);
            }
            Console.WriteLine("oscour");
        }
        Console.WriteLine("players : ");
        this.Players = players;
        Console.WriteLine("players : " + this.Players[0].Id);
        return players;
    }


    public async Task<List<UserInGame>> AlgoFormation(List<UserInGame> Players)
    {
        List<List<UserInGame>> UserListArray = new List<List<UserInGame>>();
        var echSize = 50;
        return await Task.Run(() =>
        {
            {
                UserListArray.Add(new List<UserInGame>(Players));
            }

            for (int i = 0; i < echSize; i++)
            {
                UserListArray.Add(new List<UserInGame>(Players));
            }
            //Console.WriteLine("-----------------");
            for (int i = 0; i < UserListArray.Count; i++)
            {
                Shuffle(UserListArray[i]);
            }
            UserListArray.Sort((x, y) => ScoreOfCibles(y) - ScoreOfCibles(x));
            //DisplayLists(UserListArray);

            Random rng = new();
            var iteration = 1;
            while (ScoreOfCibles(UserListArray[0]) <= UserListArray.Count * 100 - 50 && iteration < 20) //Tant que le score de la meilleure liste est inférieur à XXX
            {
                for (int i = echSize / 10; i < echSize; i++) //On garde les 10% meilleures listes
                {
                    int rngIndex = rng.Next(echSize / 10); //On prend une liste aléatoire parmi les 10% meilleures
                    UserListArray[i] = RandomSwap(UserListArray[rngIndex], rng.Next(3) + 1); //On la modifie aléatoirement
                }

                Console.WriteLine("-----------------");
                UserListArray.Sort((x, y) => ScoreOfCibles(y) - ScoreOfCibles(x));
                //DisplayLists(UserListArray);
                iteration++;
            }
        }).ContinueWith((b) =>
        {
            return UserListArray[0];
        });

    }
    public void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public List<T> RandomSwap<T>(List<T> l, int itération)
    {
        Random rng = new();
        List<T> list = new(l);
        int n = list.Count;
        for (int i = 0; i < itération; i++)
        {
            int k = rng.Next(n);
            int k2 = rng.Next(n);
            T value = list[k];
            list[k] = list[k2];
            list[k2] = value;
        }
        return list;
    }

    public void DisplayLists(List<List<UserInGame>> UserListArray)
    {
        for (int i = 0; i < UserListArray.Count; i++)
        {
            Console.Write("score : " + ScoreOfCibles(UserListArray[i]));
            foreach (UserInGame player in UserListArray[i])
            {
                Console.Write("|" + player.Famille);
            }
            Console.WriteLine("");
        }
    }
    public int ScoreOfCibles(List<UserInGame> Players)
    {
        int depth = (this.Equipes.Count - 1) * 2;
        int score = 100 * Players.Count;
        int index = 0;
        foreach (UserInGame player in Players)
        {
            for (int i = 1; i <= depth; i++)
            {
                if (Players[IndexLooper(index + i, Players)].Famille == player.Famille) score -= (int)Math.Round(100 / Math.Pow(i, 2));
            }
            index++;
        }
        return score;
    }
    public static int IndexLooper<T>(int index, IList<T> values)
    {
        if (index < 0) return values.Count + index;
        if (index >= values.Count) return index - values.Count;
        return index;
    }
}