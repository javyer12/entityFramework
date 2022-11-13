namespace entity.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// ctrl + shift + p cuando hay problema de Omni Sharp
public class Category
{
    // [Key]
    //estas son las propiedades de la tabla
    public Guid CategoryId { get; set; }
    //  [Required]
    //  [MaxLength(100)] //nombres no mas de 100 catacteres
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Relevance { get; set; }
    //esta coleccion crea la relacion con tareas
    [JsonIgnore]
    public virtual ICollection<Task>? Task { get; set; }
}

// ICollection tiene especializado que expone los objetos asi como los arrays.
// los modelos son las tablas dentro de las bases de datos

