public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public User(string username, string password, string prenom, string nom)
    {
        Username = username;
        Password = password;
        Prenom = prenom;
        Nom = nom;
    }
}