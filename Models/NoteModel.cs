using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyNotesApp.Models;
public class NoteModel{
    [Key]
    public int NoteId { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Content { get; set; }
    public int UserId { get; set; }
     
    [ForeignKey("UserId")]
    public UserModel? User { get; set; } 
}