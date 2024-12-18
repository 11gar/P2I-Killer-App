using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;
using System;
using System.Globalization;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiProjet.Controllers;

[ApiController]
[Route("api/objets")]
public class ObjetController : ControllerBase
{
    private readonly ApiContext _context;

    public ObjetController(ApiContext context)
    {
        _context = context;
    }

    // GET: api/objets
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Objet>>> GetObjets()
    {
        var objets = await _context.Objets.ToListAsync();
        return objets;
    }

    // GET: api/objets/{id}
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Objet>> GetObjet(int id)
    {
        var objet = await _context.Objets.SingleOrDefaultAsync(t => t.Id == id);
        if (objet == null)
        {
            return NotFound();
        }
        return objet;
    }

    [Authorize]
    [HttpGet("game/{id}")]
    public async Task<ActionResult<IEnumerable<Objet>>> GetObjetsOfGame(int id)
    {
        var objets = await _context.Objets.Where(t => t.IdGame == id).ToListAsync();
        if (objets == null)
        {
            return StatusCode(201, "Pas d'objets");
        }
        return objets;
    }


    [Authorize]
    [HttpGet("game/{id}/date/{date}")]
    public async Task<ActionResult<Objet>> GetObjetsOfGame(int id, string date)
    {
        if (!DateTime.TryParse(date, out DateTime actualDate))
        {
            return StatusCode(400, "Invalid date format");
        }
        var objets = await _context.Objets.Where(t => t.IdGame == id && t.DebutValidite <= actualDate && t.FinValidite >= actualDate).ToListAsync();
        if (objets.Count == 0)
        {
            return null;
        }
        if (objets.Count > 1)
        {
            objets = objets.OrderByDescending(t => t.DebutValidite).ToList();
        }

        return objets[0];
    }

    [Authorize]
    [HttpGet("game/{id}/current/{UTC}")]
    public async Task<ActionResult<Objet>> GetObjetNow(int id, int UTC)
    {
        return await GetObjetsOfGame(id, DateTime.UtcNow.AddHours(UTC).ToString());
    }


    // POST: api/objets
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Objet>> PostObjet(string nom, string description, int idGame, string debutValidite, string finValidite)
    {
        if (!DateTime.TryParse(debutValidite, out DateTime dateDebutValidite))
        {
            return StatusCode(400, "Invalid date format debut");
        }
        if (!DateTime.TryParse(finValidite, out DateTime dateFinValidite))
        {
            return StatusCode(400, "Invalid date format fin");
        }
        var objet = new Objet(nom, description, idGame, dateDebutValidite, dateFinValidite);
        _context.Objets.Add(objet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetObjet), new { id = objet.Id }, objet);
    }

    // PUT: api/objets/{id}ù
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutObjet(int id, Objet objet)
    {
        if (id != objet.Id)
        {
            return BadRequest();
        }

        _context.Entry(objet).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ObjetExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/objets/{id}
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteObjet(int id)
    {
        var objet = await _context.Objets.FindAsync(id);
        if (objet == null)
        {
            return NotFound();
        }

        _context.Objets.Remove(objet);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ObjetExists(int id)
    {
        return _context.Objets.Any(e => e.Id == id);
    }
}