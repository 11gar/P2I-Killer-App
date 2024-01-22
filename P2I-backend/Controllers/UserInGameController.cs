using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjet.Controllers;

[ApiController]
[Route("api/usersInGame")]
public class UserInGameController : ControllerBase
{
    private readonly ApiContext _context;

    public UserInGameController(ApiContext context)
    {
        _context = context;
    }

    // GET: api/userInGame
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserInGame>>> GetUsersIG()
    {
        // Get documents and related lists
        var usersInGame = await _context.UsersInGames.ToListAsync();
        var usersInGameDTO = new List<UserInGameDTO>();
        foreach (var user in usersInGame)
        {
            var u = GetUserIG(user.Id);
            if (u.Result.Value != null) usersInGameDTO.Add(u.Result.Value);
        }
        return usersInGame;
    }

    // GET: api/userInGame/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserInGameDTO>> GetUserIG(int id)
    {
        // Find document and related list
        // SingleAsync() throws an exception if no document is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var userInGame = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == id);
        if (userInGame == null)
        {
            return NotFound();
        }
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == userInGame.IdGame);
        if (game == null)
        {
            return StatusCode(400, "Game not found");
        }

        var userDTO = new UserInGameDTO(userInGame)
        {
            Cible = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == userInGame.IdCible),
            Game = game
        };

        var kills = await _context.Kills.Where(t => t.IdKiller == userInGame.IdUser).ToListAsync();
        foreach (var kill in kills)
        {
            var u = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == kill.IdKilled);
            if (u != null) userDTO.KillsList.Add(u);
        }

        return userDTO;
    }

    [HttpGet("users/{id}")]

    public async Task<ActionResult<IEnumerable<UserInGameDTO>>> GetUserIGFromIDUser(int id)
    {
        var usersInGame = await _context.UsersInGames.Where(t => t.IdUser == id).ToListAsync();
        var usersInGameDTO = new List<UserInGameDTO>();
        foreach (UserInGame userInGame in usersInGame)
        {
            var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == userInGame.IdGame);
            if (game == null)
            {
                return StatusCode(400, "Game not found, id : " + userInGame.IdGame);
            }

            var userDTO = new UserInGameDTO(userInGame)
            {
                Cible = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == userInGame.IdCible),
                Game = game
            };

            var kills = await _context.Kills.Where(t => t.IdKiller == userInGame.IdUser).ToListAsync();
            foreach (var kill in kills)
            {
                var u = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == kill.IdKilled);
                if (u != null) userDTO.KillsList.Add(u);
            }
            usersInGameDTO.Add(userDTO);
        }
        return usersInGameDTO;
    }

    [HttpGet("game/{id}")]
    public async Task<ActionResult<IEnumerable<UserInGameDTO>>> GetUserIGFromIDGame(int id)
    {
        var usersInGame = await _context.UsersInGames.Where(t => t.IdGame == id).ToListAsync();
        var usersInGameDTO = new List<UserInGameDTO>();
        foreach (UserInGame userInGame in usersInGame)
        {
            var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
            if (game == null)
            {
                return StatusCode(400, "Game not found, id : " + userInGame.IdGame);
            }

            var userDTO = new UserInGameDTO(userInGame)
            {
                Cible = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == userInGame.IdCible),
                Game = game
            };

            var kills = await _context.Kills.Where(t => t.IdKiller == userInGame.IdUser).ToListAsync();
            foreach (var kill in kills)
            {
                var u = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == kill.IdKilled);
                if (u != null) userDTO.KillsList.Add(u);
            }
            usersInGameDTO.Add(userDTO);
        }
        return usersInGameDTO;
    }







    // POST: api/userInGame
    [HttpPost]
    public async Task<ActionResult<UserInGame>> PostUserIG(UserInGame userInGame)
    {
        _context.UsersInGames.Add(userInGame);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserIG), new { id = userInGame.Id }, userInGame);
    }

    // PUT: api/userInGame/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserIG(int id, UserInGame userInGame)
    {
        _context.Entry(userInGame).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.UsersInGames.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }
}