using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;
using ApiProjet.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ApiProjet.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly AuthHelpers _authHelpers;

    public UserController(ApiContext context, AuthHelpers authHelpers)
    {
        _context = context;
        _authHelpers = authHelpers;
    }


    // GET: api/user
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        //tu peux récupérer l'id de la connexion comme ceci
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

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

    [Authorize]
    [HttpGet("{id}/canModerate/{gameId}")]
    public async Task<ActionResult<bool>> CanModerate(int id, int gameId)
    {
        var mod = await _context.Moderators.SingleOrDefaultAsync(t => t.IdModerator == id && t.IdGame == gameId);
        if (mod == null)
        {
            return false;
        }
        return true;
    }



    // GET: api/user/5
    [Authorize]
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
        userDTO.Password = "*********";
        return userDTO;
    }

    public class LoginResultDTO
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }

    [Authorize]
    [HttpGet("islogged")]
    public async Task<ActionResult<int>> IsLogged()
    {
        // Retrieve the user ID from the claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Check if userIdClaim is null or invalid
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized("Invalid credentials");
        }

        // Retrieve the user from the database
        var user = await _context.Users.SingleOrDefaultAsync(t => t.Id == userId);
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }

        // If the user is found, respond with 200 status code and body of 1
        return Ok(1);
    }

    [HttpGet("login")]
    public async Task<ActionResult<LoginResultDTO>> GetUserWithPass(string login, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(t => t.Username == login && t.Password == password);
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }

        // Generate the JWT token
        var token = _authHelpers.GenerateJWTToken(user);

        // Return both UserId and Token
        var result = new LoginResultDTO
        {
            UserId = user.Id,
            Token = token
        };

        return Ok(result);
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
    [Authorize]
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