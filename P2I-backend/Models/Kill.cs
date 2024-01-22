public class Kill
{
    public int Id { get; set; }
    public int IdGame { get; set; }
    public int IdKiller { get; set; }
    public int IdKilled { get; set; }
    public Kill(int idGame, int idKiller, int idKilled)
    {
        IdGame = idGame;
        IdKiller = idKiller;
        IdKilled = idKilled;
    }
}