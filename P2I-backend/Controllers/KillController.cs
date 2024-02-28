using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;

namespace ApiProjet.Controllers;

[ApiController]
[Route("api/kills")]
public class KillController : ControllerBase
{
    private readonly ApiContext _context;

    public KillController(ApiContext context)
    {
        _context = context;
    }

    // GET: api/kills
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Kill>>> GetKills()
    {
        var kills = await _context.Kills.ToListAsync();
        return kills;
    }



    // GET: api/kills/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Kill>> GetKill(int id)
    {
        var kill = await _context.Kills.SingleOrDefaultAsync(t => t.Id == id);
        if (kill == null)
        {
            return NotFound();
        }
        return kill;
    }

    [HttpGet("killer/{id}")]
    public async Task<ActionResult<List<Kill>>> GetKillsOfUser(int id)
    {
        var kill = await _context.Kills.Where(t => t.IdKiller == id && t.Confirmed == true).ToListAsync();
        if (kill == null)
        {
            return StatusCode(201, "Pas de kills");
        }
        return kill;
    }
    [HttpGet("game/{id}")]
    public async Task<ActionResult<List<Kill>>> GetKillsOfGame(int id)
    {
        var kill = await _context.Kills.Where(t => t.IdGame == id).ToListAsync();
        if (kill == null)
        {
            return StatusCode(201, "Pas de kills");
        }
        return kill;
    }

    [HttpGet("killed/{id}")]
    public async Task<ActionResult<Kill>> IsUserKilled(int id)
    {
        var kill = await _context.Kills.SingleOrDefaultAsync(t => t.IdKilled == id);
        if (kill == null)
        {
            return NoContent();
        }
        return kill;
    }

    [HttpGet("killing/{id}")]

    public async Task<ActionResult<Kill>> IsUserKilling(int id)
    {
        var kill = await _context.Kills.SingleOrDefaultAsync(t => t.IdKiller == id && t.Confirmed == false);
        if (kill == null)
        {
            return NoContent();
        }
        return kill;
    }


    [HttpPost]
    public async Task<ActionResult<Kill>> PostKill(int idKilled, int idKiller, int idGame)
    {
        if (idGame == 0 || idKiller == 0 || idKilled == 0) return StatusCode(414, "idGame, idKiller or idKilled is null");
        var killExists = await _context.Kills.Where(t => t.IdKilled == idKilled && t.IdKiller == idKiller && t.IdGame == idGame).ToListAsync();
        if (killExists.Count != 0) return StatusCode(410, "Kill already exists");
        Kill kill = new(
            idGame,
            idKiller,
            idKilled);
        _context.Kills.Add(kill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetKill), new { id = kill.Id }, kill);
    }

    [HttpPut("confirm/{id}")]

    public async Task<ActionResult<Kill>> ConfirmKill(int id)
    {
        var kill = await _context.Kills.SingleOrDefaultAsync(t => t.Id == id);
        if (kill == null)
        {
            return StatusCode(414, "Kill not found");
        }
        kill.Confirmed = true;

        var killed = await _context.UsersInGames.SingleOrDefaultAsync(t => t.IdUser == kill.IdKilled);
        if (killed == null)
        {
            return StatusCode(414, "Killed not found");
        }
        killed.Alive = false;

        var killer = await _context.UsersInGames.SingleOrDefaultAsync(t => t.IdUser == kill.IdKiller);
        if (killer == null)
        {
            return StatusCode(4014, "Killer not found");
        }
        killer.IdCible = killed.IdCible;

        _context.UsersInGames.Update(killer);
        _context.UsersInGames.Update(killed);
        _context.Kills.Update(kill);
        await _context.SaveChangesAsync();
        return kill;
    }

    [HttpPut("cancel/{id}")]
    public async Task<ActionResult<Kill>> DeleteKill(int id)
    {
        var kill = await _context.Kills.SingleOrDefaultAsync(t => t.Id == id);
        if (kill == null)
        {
            return NotFound();
        }
        _context.Kills.Remove(kill);
        await _context.SaveChangesAsync();
        return kill;
    }

}