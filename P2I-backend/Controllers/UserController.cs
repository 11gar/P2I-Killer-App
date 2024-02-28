using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;

namespace ApiProjet.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ApiContext _context;

    public UserController(ApiContext context)
    {
        _context = context;
    }

    // GET: api/user
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        var playing = await _context.UsersInGames.ToListAsync();
        var usersDTO = new List<UserDTO>();
        foreach (User user in users)
        {
            var userDTO = new UserDTO(user);
            userDTO.Games = new List<Game>();
            foreach (UserInGame uig in playing)
            {
                if (uig.IdUser == user.Id)
                {
                    var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == uig.IdGame);
                    if (game != null) userDTO.Games.Add(game);
                }
            }
            //FOR SAFETY REASONS, PASSWORD IS NOT SENT
            userDTO.Password = "*******";
            usersDTO.Add(userDTO);
        }
        return usersDTO;
    }



    // GET: api/user/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(t => t.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        var playing = await _context.UsersInGames.Where(t => t.IdUser == user.Id).ToListAsync();
        var userDTO = new UserDTO(user);
        userDTO.Games = new List<Game>();
        foreach (UserInGame uig in playing)
        {
            if (uig.IdUser == user.Id)
            {
                var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == uig.IdGame);
                if (game != null) userDTO.Games.Add(game);
            }
        }
        return userDTO;
    }

    [HttpGet("login")]

    public async Task<ActionResult<int>> GetUserWithPass(string login, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(t => t.Username == login && t.Password == password);
        if (user == null)
        {
            return -1;
        }
        return user.Id;
    }

    [HttpGet("login/{login}")]

    public async Task<ActionResult<int>> GetUserByLogin(string login)
    {
        var user = await _context.Users.Where(t => t.Username == login).ToListAsync();
        if (user.Count == 0)
        {
            return -1;
        }
        return 1;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> PostUser(string login, string password, string prenom, string nom)
    {
        User user = new(
            login,
            password,
            prenom,
            nom);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/user/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }


}