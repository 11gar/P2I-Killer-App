using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;
using System;
using System.Globalization;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Objet>>> GetObjets()
    {
        var objets = await _context.Objets.ToListAsync();
        return objets;
    }

    // GET: api/objets/{id}
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
    [HttpGet("game/{id}/time")]
    public async Task<ActionResult<IEnumerable<Objet>>> GetObjetsOfGame(int id, string date)
    {
        if (!DateTime.TryParse(date, out DateTime actualDate))
        {
            return StatusCode(400, "Invalid date format");
        }
        var objets = await _context.Objets.Where(t => t.IdGame == id && t.DebutValidite <= actualDate && t.FinValidite >= actualDate).ToListAsync();
        if (objets == null)
        {
            return StatusCode(201, "Pas d'objets");
        }
        return objets;
    }

    // POST: api/objets
    [HttpPost]
    public async Task<ActionResult<Objet>> PostObjet(string Nom, string description, int idGame, string debutValidite, string finValidite)
    {
        if (!DateTime.TryParse(debutValidite, out DateTime dateDebutValidite))
        {
            return StatusCode(400, "Invalid date format debut");
        }
        if (!DateTime.TryParse(finValidite, out DateTime dateFinValidite))
        {
            return StatusCode(400, "Invalid date format fin");
        }
        var objet = new Objet(Nom, description, idGame, dateDebutValidite, dateFinValidite);
        _context.Objets.Add(objet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetObjet), new { id = objet.Id }, objet);
    }
    // PUT: api/objets/{id}
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