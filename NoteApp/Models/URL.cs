using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models;

public class URL
{
    public int Id { get; set; }

    [Required]
    public int NoteId { get; set; }
    [Required]
    public string Short { get; set; }
}