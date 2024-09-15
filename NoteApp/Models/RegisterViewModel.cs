using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models;

public class RegisterViewModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}