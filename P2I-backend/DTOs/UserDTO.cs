public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Game> Games { get; set; }
    public UserDTO(User user)
    {
        Id = user.Id;
        Username = user.Username;
        Password = user.Password;
        Games = [];
    }
}