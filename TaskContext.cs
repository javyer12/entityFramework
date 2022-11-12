using Microsoft.EntityFrameworkCore;
namespace entity;
using entity.Models;

public class TaskContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    //sobre escribir el metodo OnModelCreating que es el que se invoca cuando se crean las tablas,
    //cuando se sobre escribe un metodo no puede ser publico debe ser protected
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //crear una colleccion llamada categoria
        // List<Category> categoriesInit = new List<Category>();
        // categoriesInit.Add(new Category() { CategoryId = Guid.Parse("e2411a88-eb28-4ea5-a220-85d5e2d4fa7a"), Name = "Pending Activities", Relevance = "40" });
        // categoriesInit.Add(new Category() { CategoryId = Guid.Parse("e2411a88-eb28-4ea5-a220-85d5e2d4fa7b"), Name = "React Activities", Relevance = "80" });


        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);

            category.Property(p => p.Name).IsRequired().HasMaxLength(150);
            category.Property(p => p.Description).IsRequired().HasMaxLength(250);
            category.Property(p => p.Relevance);
            // category.HasData(categoriesInit);
        });

        // List<Task> taskInit  = new List<Task>();
        //el id de la categoria id en task debe ser el mismo id de la categoria
        // taskInit.Add(new Task() {TaskId = Guid.Parse("e2411a88-eb28-4ea5-a220-85d5e2d4fa71"), CategoryId = Guid.Parse("e2411a88-eb28-4ea5-a220-85d5e2d4fa7b"), PriorityTask = Priority.Middle,})
        modelBuilder.Entity<Task>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);

            //indicar la llave foranea y relacion de ambos modelos
            task.HasOne(p => p.Category).WithMany(p => p.Task).HasForeignKey(p => p.CategoryId);

            task.Property(p => p.Title).IsRequired().HasMaxLength(200);

            task.Property(p => p.Description).IsRequired().HasMaxLength(250);

            task.Property(p => p.CreationDate);

            task.Property(p => p.PriorityTask);

            task.Property(p => p.DeadLine);

            task.Ignore(p => p.Resumen);
        });
    }
}
// los nombres de forma plural

// Contexto: Es donde van a ir todas las relaciones de los modelos que nosotros tenemos para poder transformarlo en colecciones que van a representarse dentro de la base de datos.
// Microsoft nos ofrece más información sobre esta clase en el siguiente enlace : https://docs.microsoft.com/es-es/ef/ef6/fundamentals/working-with-dbcontext#defining-a-dbcontext-derived-class

// DBSet: Es un set o una asignación de datos del modelo que nosotros hemos creado previamente, básicamente esto va a representar lo que sería una tabla dentro del contexto de entity framework.
// Un DbSet representa la colección de todas las entidades en el contexto, o que se puede consultar desde la base de datos, de un tipo determinado. Los objetos DbSet se crean a partir de DbContext mediante el método DbContext. set. Microsoft nos ofrece más información sobre esta clase en el siguiente enlace: https://docs.microsoft.com/es-es/dotnet/api/system.data.entity.dbset-1?view=entity-framework-6.2.0