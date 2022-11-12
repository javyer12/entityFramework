using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using entity;

var builder = WebApplication.CreateBuilder(args);
//base de datos en memorias
// builder.Services.AddDbContext<TaskContext>(p => p.UseInMemoryDatabase("tasks db"));

//configuracion en el mismo archivo usar addDbContext y UseNpgsql
//configuracion en appsettings.json usar AddNpgsql y GetConnectionString.
//Postgre sql 
//TaskContext es el modelo de la base de datos que queremos crear
builder.Services.AddNpgsql<TaskContext>(builder.Configuration.GetConnectionString("TaskDb"));
//configuration on main file, bad practice.
// builder.Services.AddDbContext<TaskContext>(options =>
//                 options.UseNpgsql("Server=postgreServer;Database=TaskEF;Port=5430;Username =my_user;Password=root;Host=localhost;")
//                );

// construccion de la app
var app = builder.Build();

//endpoints
app.MapGet("/", () => "Hello World!");
app.MapGet("/entity", () => "Entity Framework!");
app.MapGet("/dbconexion", async ([FromServices] TaskContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Now we are connected to Postgres using Fluent API: " + dbContext.Database.IsInMemory());
});
app.Run();
//dotnet dev-certs https --clean    
//dotnet dev-certs https