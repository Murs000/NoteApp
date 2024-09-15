using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models;

public class EmailConfirmationViewModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Token { get; set; }
}