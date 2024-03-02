public class Objet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public int IdGame { get; set; }
    public DateTime DebutValidite { get; set; }
    public DateTime FinValidite { get; set; }
    public Objet()
    {
        Name = "";
        Details = "";
        IdGame = 0;
        DebutValidite = DateTime.Now;
        FinValidite = DateTime.Today.AddDays(1);
    }
    public Objet(string name, string description, int idgame, DateTime debut, DateTime fin)
    {
        Name = name;
        Details = description;
        IdGame = idgame;
        DebutValidite = debut;
        FinValidite = fin;
    }
    public Objet(string name, string description, int idgame)
    {
        Name = name;
        Details = description;
        IdGame = idgame;
        DebutValidite = DateTime.Now;
        FinValidite = DateTime.Today.AddDays(1);
    }
}