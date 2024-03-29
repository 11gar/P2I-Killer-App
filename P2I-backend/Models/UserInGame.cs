public class UserInGame
{
    public int Id { get; set; }
    public int IdGame { get; set; }
    public int IdUser { get; set; }
    public bool Alive { get; set; }
    public int Kills { get; set; }
    public int IdCible { get; set; }
    public int Famille { get; set; }

    public UserInGame()
    {
        IdGame = 0;
        IdUser = 0;
        IdCible = 0;
        Alive = true;
        Kills = 0;
        Famille = 0;
    }
    public UserInGame(Game game, User user, UserInGame cible)
    {
        IdGame = game.Id;
        IdUser = user.Id;
        IdCible = cible.Id;
        Alive = true;
        Kills = 0;
        Famille = 0;
    }
    public UserInGame(int gameid, int userid)
    {
        IdGame = gameid;
        IdUser = userid;
        IdCible = 0;
        Alive = true;
        Kills = 0;
        Famille = 0;
    }
    public UserInGame(int gameid, int userid, int famille)
    {
        IdGame = gameid;
        IdUser = userid;
        IdCible = 0;
        Alive = true;
        Kills = 0;
        Famille = famille;
    }
}