using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiProjet.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamController : ControllerBase
{
    private readonly ApiContext _context;

    public TeamController(ApiContext context)
    {
        _context = context;
    }


    [Authorize]
    [HttpGet("game/{id}")]
    public async Task<ActionResult<IEnumerable<Equipe>>> GetTeamsFromGame(int id)
    {
        var teams = await _context.Equipes.Where(t => t.IdGame == id).ToListAsync();
        return teams;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Equipe>> PostTeam(string nom, string couleur, int idGame)
    {
        var team = new Equipe(idGame, nom, couleur);
        _context.Equipes.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Equipe>> DeleteTeam(int id)
    {
        var players = await _context.UsersInGames.Where(t => t.Famille == id).ToListAsync();
        foreach (var player in players)
        {
            player.Famille = 0;
        }
        var equipe = await _context.Equipes.FindAsync(id);
        if (equipe == null)
        {
            return NotFound();
        }

        _context.Equipes.Remove(equipe);
        await _context.SaveChangesAsync();

        return equipe;
    }

}