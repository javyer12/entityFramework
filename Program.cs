using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using entity;
using entity.Models;
using Task = entity.Models.Task;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
app.MapGet("/dbconexion", ([FromServices] TaskContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Now we are connected to Postgres using Fluent API: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/task", ([FromServices] TaskContext dbContext) =>
{
    return Results.Ok(dbContext.Tasks.Include(p => p.Category));
});
app.MapGet("/api/category", ([FromServices] TaskContext dbContext) =>
{
    return Results.Ok(dbContext.Categories);
});


app.MapPost("/api/task", async ([FromServices] TaskContext dbContext, [FromBody] Task task) =>
{
    task.TaskId = Guid.NewGuid();
    task.CreationDate = DateTime.Now;
    //estas son las dos maneras para agregar la informacion
    await dbContext.AddAsync(task);
    // await dbContext.Tasks.AddAsync(task);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/api/task/{id}", async ([FromServices] TaskContext dbContext, [FromBody] Task task, [FromRoute] Guid id) =>
{
    var currentTask = dbContext.Tasks.Find(id);
    if (currentTask != null)
    {
        currentTask.CategoryId = task.CategoryId;
        currentTask.Title = task.Title;
        currentTask.Description = task.Description;
        currentTask.PriorityTask = task.PriorityTask;
        currentTask.DeadLine = task.DeadLine;

        await dbContext.SaveChangesAsync();
        return Results.Ok($"Task was updated successful");
    }
    //el id no debe de cambiar
    return Results.NotFound();
});

app.MapDelete("/api/task/{id}", async ([FromServices] TaskContext dbContext, [FromRoute] Guid id) =>
{
    var currentTask = dbContext.Tasks.Find(id);
    if (currentTask != null)
    {
        dbContext.Remove(currentTask);
        await dbContext.SaveChangesAsync();

        return Results.Ok($"Task was deleted successfully");
    }
    return Results.NotFound($"Task NotFound");
});
app.Run();
//dotnet dev-certs https --clean    
//dotnet dev-certs https