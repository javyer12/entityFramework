namespace entity.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Email { get; set; }
}

//nombre de los archivos en single