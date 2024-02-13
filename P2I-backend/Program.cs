using ApiProjet.Data;
using ApiProjet.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


// var users = new List<User>
//         {
//             new("user1", "password1","pre","nom"),
//             new("user2", "password2","pre","nom"),
//             new("user3", "password3","pre","nom"),
//         };
// Game game = new("game1", "game1");
// GameDTO gamedto = new(game)
// {
//     NumberOfTeams = 5
// };

// var usersInGames = new List<UserInGame>
// {
//     new UserInGame(1,1,1),
//     new UserInGame(2,1,1),
//     new UserInGame(3,1,1),
//     new UserInGame(4,1,2),
//     new UserInGame(5,1,2),
//     new UserInGame(6,1,2),
//     new UserInGame(7,1,3),
//     new UserInGame(8,1,3),
//     new UserInGame(9,1,3),
//     new UserInGame(10,1,2),
//     new UserInGame(1,1,1),
//     new UserInGame(2,1,1),
//     new UserInGame(3,1,1),
//     new UserInGame(4,1,4),
//     new UserInGame(5,1,5),
//     new UserInGame(6,1,2),
//     new UserInGame(7,1,3),
//     new UserInGame(8,1,3),
//     new UserInGame(9,1,3),
//     new UserInGame(10,1,2),
//     new UserInGame(4,1,5),
//     new UserInGame(5,1,5),
//     new UserInGame(6,1,4),
//     new UserInGame(7,1,3),
//     new UserInGame(8,1,3),
//     new UserInGame(9,1,3),
//     new UserInGame(10,1,4),
// };
// gamedto.Players = usersInGames;
// gamedto.InitCibles();

// foreach (UserInGame player in gamedto.Players)
// {
//     Console.WriteLine(player.Id + " " + player.Famille);
// }


// Console.WriteLine("coucou");



var builder = WebApplication.CreateBuilder(args);
SeedData.Init();
Console.WriteLine($"caca");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>();

var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder =>
    {
        //builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        //builder.SetIsOriginAllowed(origin => true);
    });
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devCorsPolicy);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();