namespace entity.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    // public Guid TaskId { get; set; }
    //intentar con virtual task
    // public virtual Task Task { get; set; }
    //collection la ultima que use
    // public virtual ICollection<Task> Task { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }
}

//nombre de los archivos en single