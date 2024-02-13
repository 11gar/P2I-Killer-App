public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Password { get; set; }
    public bool IsStarted { get; set; }
    public int NumberOfTeams;

    public Game()
    {
        Name = "";
        Password = null;
        IsStarted = false;
        NumberOfTeams = 0;
    }
    public Game(string name, string? password)
    {
        Name = name;
        Password = password;
        IsStarted = false;
        NumberOfTeams = 0;
    }

}