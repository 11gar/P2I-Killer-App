public class UserInGame
{
    public int Id { get; set; }
    public int IdGame { get; set; }
    public int IdUser { get; set; }
    public bool Alive { get; set; }
    public int Kills { get; set; }
    public int IdCible { get; set; }

    public UserInGame()
    {
        IdGame = 0;
        IdUser = 0;
        Alive = true;
        Kills = 0;
    }
    public UserInGame(Game game, User user, UserInGame cible)
    {
        IdGame = game.Id;
        IdUser = user.Id;
        IdCible = cible.Id;
        Alive = true;
        Kills = 0;
    }
    public UserInGame(Game game, User user)
    {
        IdGame = game.Id;
        IdUser = user.Id;
        IdCible = 0;
        Alive = true;
        Kills = 0;
    }

}