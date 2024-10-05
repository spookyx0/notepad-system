using System.ComponentModel.DataAnnotations;
namespace MyNotesApp.Models;
public class UserModel{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
      public List<NoteModel>? Notes { get; set; }
}