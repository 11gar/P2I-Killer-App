public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsStarted { get; set; }
    public int NumberOfTeams;

    public Game()
    {
        Name = "";
        IsStarted = false;
        NumberOfTeams = 0;
    }

    public Game(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public Game(string name)
    {
        Name = name;
        IsStarted = false;
        NumberOfTeams = 0;
    }

}