public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Password { get; set; }
    public bool IsStarted { get; set; }
    public Game()
    {
        Name = "";
        Password = null;
        IsStarted = false;
    }
    public Game(string name, string? password)
    {
        Name = name;
        Password = password;
        IsStarted = false;
    }

}