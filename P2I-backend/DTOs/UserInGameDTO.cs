public class UserInGameDTO
{
    public int Id { get; set; }
    public bool Alive { get; set; }
    public User User { get; set; }
    public Game Game { get; set; }
    public List<UserInGame> KillsList { get; set; }
    public UserInGame? Cible { get; set; }
    public Equipe? Equipe { get; set; }

    public UserInGameDTO()
    {
        Game = new Game();
        User = new User("def", "def", "prenom", "nom");
        Alive = true;
        Cible = null;
        KillsList = new List<UserInGame>();
    }
    public UserInGameDTO(Game game, User user, UserInGame cible)
    {
        User = user;
        Game = game;
        Alive = true;

        Cible = cible;
        KillsList = new List<UserInGame>();
    }
    public UserInGameDTO(Game game, User user)
    {
        User = user;
        Game = game;
        Alive = true;
        Cible = null;
        KillsList = new List<UserInGame>();
    }
    public UserInGameDTO(UserInGame userInGame)
    {
        Id = userInGame.Id;
        User = new User("def", "def", "prenom", "nom")
        {
            Id = userInGame.IdUser
        };
        Game = new Game();
        Alive = userInGame.Alive;
        Cible = null;
        KillsList = new List<UserInGame>();
    }
}