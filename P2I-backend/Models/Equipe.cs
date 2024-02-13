public class Equipe
{
    public int Id { get; set; }
    public int IdGame { get; set; }
    public string Name { get; set; }

    public Equipe()
    {
        IdGame = 0;
        Name = "";
    }
    public Equipe(int idgame, string name)
    {
        IdGame = idgame;
        Name = name;
    }
}