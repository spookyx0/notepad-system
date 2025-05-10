using System.ComponentModel.DataAnnotations;
using MyNotesApp.Data;
namespace MyNotesApp.Models
{
   public class LoginModel{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
} 