public class GameDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsStarted { get; set; }
    public List<UserInGame> Players { get; set; }
    public List<User> Moderators { get; set; }

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
        Players = new List<UserInGame>();
        Moderators = new List<User>();
    }

    public void InitCibles(List<User> Players)
    {

    }

    public int ScoreOfCibles(List<User> Players)
    {
        return 0;
    }


}