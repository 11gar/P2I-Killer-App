using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
    {
        // Get documents and related lists
        var games = await _context.Games.ToListAsync();
        var gamesDTO = new List<GameDTO>();
        foreach (var game in games)
        {
            gamesDTO.Add(new GameDTO(game));
        }
        return gamesDTO;
    }

    // GET: api/game/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDTO>> GetGame(int id)
    {
        // Find document and related list
        // SingleAsync() throws an exception if no document is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
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
            if (m2 != null) gameDTO.Moderators.Add(m2);
        }

        return gameDTO;
    }

    // POST: api/game
    [HttpPost("create")]
    public async Task<ActionResult<Game>> PostGame(string name, string? password)
    {
        if (password == "") password = null;
        var game = new Game(name, password);
        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
    }

    [HttpPut("{id}/shuffle")]
    public async Task<ActionResult<Game>> ShuffleGame(int id)
    {
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
        if (game == null)
        {
            return StatusCode(400, "Kill not found");
        }
        var gameDTO = new GameDTO(game);
        gameDTO.Players = await _context.UsersInGames.Where(t => t.IdGame == game.Id).ToListAsync();
        await gameDTO.InitCibles().ContinueWith((b) =>
        {
            var i = 0;
            foreach (var u in gameDTO.Players)
            {
                u.IdCible = gameDTO.Players[GameDTO.IndexLooper(i + 1, gameDTO.Players)].Id;
                _context.UsersInGames.Update(u);
                i++;
            }
        });
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
        return game;
    }



    // PUT: api/game/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, Game game)
    {
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