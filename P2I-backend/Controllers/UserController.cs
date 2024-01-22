using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        // Get documents and related lists
        var users = await _context.Users.ToListAsync();
        return users;
    }

    // GET: api/user/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        // Find document and related list
        // SingleAsync() throws an exception if no document is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var user = await _context.Users.SingleOrDefaultAsync(t => t.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    // POST: api/user
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
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