using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _3ASP.Enums;

namespace _3ASP.Models;

public class User
{
    [Key]
    public int Id { get;  set; }

    [Column("Email")]
    [Required]
    public string Email { get;  set; }

    [Column("Pseudo")]
    [Required]
    public string Pseudo { get;  set; }

    [Column("Password")]
    [Required]
    public string Password { get;  set; }

    [Column("Role")]
    public Roles Role { get; private set; } = Roles.User;
    
}