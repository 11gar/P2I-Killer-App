public class Equipe
{
    public int Id { get; set; }
    public int IdGame { get; set; }
    public string Name { get; set; }
    public string Couleur { get; set; }

    public Equipe()
    {
        IdGame = 0;
        Name = "";
        Couleur = "#000000";
    }
    public Equipe(int idgame, string name, string couleur)
    {
        IdGame = idgame;
        Name = name;
        Couleur = couleur;
    }
}