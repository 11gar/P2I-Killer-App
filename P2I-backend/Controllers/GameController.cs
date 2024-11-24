using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiProjet.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly ApiContext _context;

    public GameController(ApiContext context)
    {
        _context = context;
    }

    // GET: api/game
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
    {
        // Get documents and related lists
        var games = await _context.Games.ToListAsync();
        var gamesDTO = new List<GameDTO>();
        foreach (var game in games)
        {
            var g = GetGame(game.Id).Result.Value;
            if (g != null) gamesDTO.Add(g);
        }
        return gamesDTO;
    }

    // GET: api/game/5
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDTO>> GetGame(int id)
    {
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
        if (game == null)
        {
            return NotFound();
        }
        var gameDTO = new GameDTO(game);
        gameDTO.Players = await _context.UsersInGames.Where(t => t.IdGame == game.Id).ToListAsync();
        gameDTO.Equipes = await _context.Equipes.Where(t => t.IdGame == game.Id).ToListAsync();
        var mod = await _context.Moderators.Where(t => t.IdGame == game.Id).ToListAsync();
        foreach (var m in mod)
        {
            var m2 = await _context.Users.SingleOrDefaultAsync(t => t.Id == m.IdModerator);
            if (m2 != null)
            {
                m2.Password = "******";
                gameDTO.Moderators.Add(m2);
            }
        }

        return gameDTO;
    }

    [Authorize]
    [HttpGet("user/{id}")]
    public async Task<ActionResult<List<Game>>> GetGamesOfUser(int id)
    {
        var usersInGame = await _context.UsersInGames.Where(t => t.IdUser == id).ToListAsync();
        var games = new List<Game>();
        foreach (var userInGame in usersInGame)
        {
            var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == userInGame.IdGame);
            if (game != null)
            {
                games.Add(game);
            }
        }
        return games;
    }

    [Authorize]
    [HttpGet("moderator/{id}")]
    public async Task<ActionResult<List<Game>>> GetGamesOfModerator(int id)
    {
        var moderators = await _context.Moderators.Where(t => t.IdModerator == id).ToListAsync();
        var games = new List<Game>();
        foreach (var moderator in moderators)
        {
            var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == moderator.IdGame);
            if (game != null)
            {
                games.Add(game);
            }
        }
        return games;
    }

    // POST: api/game
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<Game>> PostGame(string name, int idCreator)
    {
        var game = new Game(name);
        var g = _context.Games.Add(game);
        await _context.SaveChangesAsync();
        var moderator = await _context.Users.SingleOrDefaultAsync(t => t.Id == idCreator);
        if (moderator == null)
        {
            return StatusCode(400, "Moderator not found");
        }
        var mod = new Moderate(g.CurrentValues.GetValue<int>("Id"), idCreator);
        _context.Moderators.Add(mod);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
    }

    [Authorize]
    [HttpPut("{id}/shuffle")]
    public async Task<ActionResult<GameDTO>> ShuffleGame(int id, string body)
    {
        // body = "298,296,294,300,302,304,306,308,310,312,314,316,318,320,322,324,326,328";
        // body = null;
        Console.WriteLine("body:" + body);
        var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (await _context.Moderators.SingleOrDefaultAsync(t => t.IdModerator == int.Parse(requesterId) && t.IdGame == id) == null)
        {
            return StatusCode(403, "Unothorized");
        }
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
        if (game == null)
        {
            return StatusCode(400, "Game not Found");
        }
        var gameDTO = new GameDTO(game);
        gameDTO.Players = await _context.UsersInGames.Where(t => t.IdGame == game.Id && t.Alive).ToListAsync();
        gameDTO.Equipes = await _context.Equipes.Where(t => t.IdGame == game.Id).ToListAsync();
        var newCibles = await gameDTO.ChangeCiblesFromCsv(body);
        gameDTO.Players = newCibles;
        var i = 0;
        foreach (var u in gameDTO.Players)
        {
            if (i == gameDTO.Players.Count - 1)
            {
                u.IdCible = gameDTO.Players[0].Id;
                _context.UsersInGames.Update(u);
                break;
            }
            else
            {
                Console.WriteLine(i);
                Console.WriteLine(gameDTO.Players[i]);
                u.IdCible = gameDTO.Players[i + 1].Id;
                _context.UsersInGames.Update(u);
            }
            i++;

        }
        await _context.SaveChangesAsync();
        return gameDTO;
    }

    public async Task<ActionResult<GameDTO>> OldShuffleGame(int id)
    {
        var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (await _context.Moderators.SingleOrDefaultAsync(t => t.IdModerator == int.Parse(requesterId) && t.IdGame == id) == null)
        {
            return StatusCode(403, "Unothorized");
        }
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
        if (game == null)
        {
            return StatusCode(400, "Game not Found");
        }
        var gameDTO = new GameDTO(game);
        gameDTO.Players = await _context.UsersInGames.Where(t => t.IdGame == game.Id && t.Alive).ToListAsync();
        gameDTO.Equipes = await _context.Equipes.Where(t => t.IdGame == game.Id).ToListAsync();
        var newCibles = await gameDTO.InitCibles();
        gameDTO.Players = newCibles;
        var i = 0;
        foreach (var u in gameDTO.Players)
        {
            if (i == gameDTO.Players.Count - 1)
            {
                u.IdCible = gameDTO.Players[0].Id;
                _context.UsersInGames.Update(u);
                break;
            }
            else
            {
                Console.WriteLine(i);
                Console.WriteLine(gameDTO.Players[i]);
                u.IdCible = gameDTO.Players[i + 1].Id;
                _context.UsersInGames.Update(u);
            }
            i++;
        }
        await _context.SaveChangesAsync();
        return gameDTO;
    }

    [Authorize]
    [HttpPut("{id}/start")]
    public async Task<ActionResult<GameDTO>> StartGame(int id)
    {
        var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (await _context.Moderators.SingleOrDefaultAsync(t => t.IdModerator == int.Parse(requesterId) && t.IdGame == id) == null)
        {
            return StatusCode(403, "Unothorized");
        }
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
        if (game == null) return StatusCode(400, "Game not found");
        game.IsStarted = true;
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
        await OldShuffleGame(id);

        var g = GetGame(id).Result.Value;
        if (g == null)
        {
            return StatusCode(400, "Game not found");
        }
        return g;
    }

    [Authorize]
    [HttpPost("{id}/moderate")]
    public async Task<ActionResult<Game>> AddModerator(int id, int idModerator)
    {
        var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (await _context.Moderators.SingleOrDefaultAsync(t => t.IdModerator == int.Parse(requesterId) && t.IdGame == id) == null)
        {
            return StatusCode(403, "Unothorized");
        }
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
        if (game == null)
        {
            return StatusCode(400, "Game not found");
        }
        var moderator = await _context.Users.SingleOrDefaultAsync(t => t.Id == idModerator);
        if (moderator == null)
        {
            return StatusCode(400, "Moderator not found");
        }
        var mod = new Moderate(id, idModerator);
        _context.Moderators.Add(mod);
        await _context.SaveChangesAsync();
        return game;
    }




    // PUT: api/game/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, Game game)
    {
        var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (await _context.Moderators.SingleOrDefaultAsync(t => t.IdModerator == int.Parse(requesterId) && t.IdGame == id) == null)
        {
            return StatusCode(403, "Unothorized");
        }
        _context.Entry(game).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Games.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }
}