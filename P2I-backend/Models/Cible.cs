public class Cible
{
    public int Id { get; set; }
    public int IdGame { get; set; }
    public int IdKiller { get; set; }
    public int IdCible { get; set; }
    public Cible(int idGame, int idKiller, int idCible)
    {
        IdGame = idGame;
        IdKiller = idKiller;
        IdCible = idCible;
    }
}