using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    public bool IsEmailConfirmed { get; set; }
}