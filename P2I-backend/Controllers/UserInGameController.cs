using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserInGame>>> GetUsersIG()
    {
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
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserInGameDTO>> GetUserIG(int id)
    {
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

        var userInGameDTO = new UserInGameDTO(userInGame)
        {
            Cible = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == userInGame.IdCible),
            Game = game,
        };

        var user = await _context.Users.SingleOrDefaultAsync(t => t.Id == userInGame.IdUser);
        if (user == null)
        {
            return StatusCode(400, "User not found");
        }
        user.Password = "*******";
        userInGameDTO.User = user;

        var equipe = await _context.Equipes.SingleOrDefaultAsync(t => t.Id == userInGame.Famille && t.IdGame == userInGame.IdGame);
        userInGameDTO.Equipe = equipe;

        var kills = await _context.Kills.Where(t => t.IdKiller == userInGame.IdUser).ToListAsync();
        foreach (var kill in kills)
        {
            var u = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == kill.IdKilled);
            if (u != null) userInGameDTO.KillsList.Add(u);
        }

        return userInGameDTO;
    }

    [Authorize]
    [HttpGet("users/{id}")]
    public async Task<ActionResult<IEnumerable<UserInGameDTO>>> GetUserIGFromIDUser(int id)
    {
        var usersInGame = await _context.UsersInGames.Where(t => t.IdUser == id).ToListAsync();
        var usersInGameDTO = new List<UserInGameDTO>();
        foreach (UserInGame userInGame in usersInGame)
        {
            var userInGameDTO = GetUserIG(userInGame.Id).Result.Value;
            if (userInGameDTO == null) return StatusCode(400, "error in GetUserIGFromIDUser Controller");
            usersInGameDTO.Add(userInGameDTO);
        }
        return usersInGameDTO;
    }



    [Authorize]
    [HttpGet("game/{id}")]
    public async Task<ActionResult<IEnumerable<UserInGameDTO>>> GetUsersIGFromIDGame(int id)
    {
        var usersInGame = await _context.UsersInGames.Where(t => t.IdGame == id).ToListAsync();
        var usersInGameDTO = new List<UserInGameDTO>();
        foreach (UserInGame userInGame in usersInGame)
        {
            var userInGameDTO = GetUserIG(userInGame.Id).Result.Value;
            if (userInGameDTO == null) return StatusCode(400, "error in GetUserIGFromIDUser Controller");
            usersInGameDTO.Add(userInGameDTO);
        }
        return usersInGameDTO;
    }

    [Authorize]
    [HttpGet("game/{id}/inorder")]
    public async Task<ActionResult<IEnumerable<UserInGameDTO>>> UsersInOrderInGame(int id)
    {
        var players = (await GetUsersIGFromIDGame(id)).Value!.ToList();
        var playersInOrder = new List<UserInGameDTO>();
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == id);
        if (!game!.IsStarted)
        {
            return players;
        }
        if (players.Count == 0) return playersInOrder;
        playersInOrder.Add(players[0]);
        var alive = players.Where(t => t.Alive).ToList();
        var dead = players.Where(t => !t.Alive).ToList();
        foreach (var player in alive)
        {
            if (player.Cible == null) continue;
            var addp = players.Find((p) => p.Id == playersInOrder[^1].Cible!.Id);
            if (addp != null) playersInOrder.Add(addp);
        }
        playersInOrder.RemoveAt(playersInOrder.Count - 1);
        playersInOrder.AddRange(dead);
        return playersInOrder;
    }


    [Authorize]
    [HttpGet("game/{idGame}/user/{idUser}")]
    public async Task<ActionResult<UserInGameDTO>> GetUserIGFromIDGameAndIDUser(int idGame, int idUser)
    {
        var userInGame = await _context.UsersInGames.SingleOrDefaultAsync(t => t.IdGame == idGame && t.IdUser == idUser);
        if (userInGame == null)
        {
            return StatusCode(400, "User " + idUser + " not found in game " + idGame);
        }
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == idGame);
        if (game == null)
        {
            return StatusCode(400, "Game not found, id : " + idGame);
        }

        var userDTO = GetUserIG(userInGame.Id).Result.Value;
        if (userDTO == null)
        {
            return StatusCode(400, "error in GetUserIGFromIDGameAndIDUser Controller");
        }
        return userDTO;
    }

    // POST: api/userInGame
    [Authorize]
    [HttpPost("join")]
    public async Task<ActionResult<UserInGame>> PostUserIG(int idGame, int idUser)
    {
        Console.WriteLine($"Joining game {idGame} with user {idUser}");
        var game = await _context.Games.SingleOrDefaultAsync(t => t.Id == idGame);
        if (game != null && game.IsStarted)
        {
            return StatusCode(400, "Game already started");
        }
        if (!_context.Games.Any(t => t.Id == idGame))
            return StatusCode(404, "Game not found, id : " + idGame);
        if (!_context.Users.Any(t => t.Id == idUser))
            return StatusCode(405, "User not found, id : " + idUser);
        if (_context.UsersInGames.Any(t => t.IdGame == idGame && t.IdUser == idUser))
        {
            Console.WriteLine($"User already in game, id : {idUser} in game {idGame}");
            return StatusCode(301, "User already in game");
        }
        var userInGame = new UserInGame(idGame, idUser);
        _context.UsersInGames.Add(userInGame);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserIG), new { id = userInGame.Id }, userInGame);
    }

    // PUT: api/userInGame/5
    [Authorize]
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

    [Authorize]
    [HttpPut("{id}/team/{teamid}")]
    public async Task<ActionResult<UserInGame>> ChangeUserTeam(int id, int teamid)
    {
        var userInGame = await _context.UsersInGames.SingleOrDefaultAsync(t => t.Id == id);
        if (userInGame == null)
        {
            return NotFound();
        }
        userInGame.Famille = teamid;
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
        return userInGame;
    }


    [Authorize]
    [HttpDelete("game/{idGame}/user/{idUser}")]
    public async Task<IActionResult> DeleteUserIG(int idGame, int idUser)
    {
        var userInGame = await _context.UsersInGames.SingleOrDefaultAsync(t => t.IdGame == idGame && t.IdUser == idUser);
        if (userInGame == null)
        {
            return NotFound();
        }
        _context.UsersInGames.Remove(userInGame);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}