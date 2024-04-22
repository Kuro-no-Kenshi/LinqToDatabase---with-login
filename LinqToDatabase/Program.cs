using LinqToDatabase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connStr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSqlServer<GameDbContext>(connStr);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Popolamento Database
using(var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetService<GameDbContext>();
    ctx.Database.Migrate();
    if (!ctx.Players.Any())
    {
        Random rand = new Random();
        for (int i = 0; i < 5000; i++)
        {
            List<Character> characters = new List<Character>();
            for(int j = 0; j < 100; j++)
            {
                characters.Add(new Character
                {
                    CharacterLevel = rand.Next(0, 1001),
                    NickName = $"Character_{i}_{j}"
                });
            }
            ctx.Players.Add(new Player
            {
                AccountLevel = rand.Next(0, 101),
                AccountName = $"Player_{i}",
                Credits = rand.Next(1000, 1000000),
                Characters = characters
            });
        }
            
        ctx.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
