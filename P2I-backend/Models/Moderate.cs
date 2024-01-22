public class Moderate
{
    public int Id { get; set; }
    public int IdGame { get; set; }
    public int IdModerator { get; set; }
    public Moderate(int idGame, int idModerator)
    {
        IdGame = idGame;
        IdModerator = idModerator;
    }
}