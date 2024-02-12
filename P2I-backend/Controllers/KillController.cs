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

    // GET: api/kill
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Kill>>> GetKills()
    {
        var kills = await _context.Kills.ToListAsync();
        return kills;
    }



    // GET: api/kill/5
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
    public async Task<ActionResult<Kill>> GetKillsOfUser(int id)
    {
        var kill = await _context.Kills.SingleOrDefaultAsync(t => t.IdKiller == id);
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
            return StatusCode(201, "Pas tué");
        }
        return kill;
    }

    [HttpPost]
    public async Task<ActionResult<Kill>> PostKill(int idKilled, int idKiller, int idGame)
    {
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
            return StatusCode(400, "Kill not found");
        }
        kill.Confirmed = true;

        var killed = await _context.UsersInGames.SingleOrDefaultAsync(t => t.IdUser == kill.IdKilled);
        if (killed == null)
        {
            return StatusCode(400, "Killed not found");
        }
        killed.Alive = false;

        var killer = await _context.UsersInGames.SingleOrDefaultAsync(t => t.IdUser == kill.IdKiller);
        if (killer == null)
        {
            return StatusCode(400, "Killer not found");
        }
        killer.IdCible = killed.IdCible;

        _context.UsersInGames.Update(killer);
        _context.UsersInGames.Update(killed);
        _context.Kills.Update(kill);
        await _context.SaveChangesAsync();
        return kill;
    }

    [HttpPut("deny/{id}")]
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