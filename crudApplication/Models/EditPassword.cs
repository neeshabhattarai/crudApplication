using System.ComponentModel.DataAnnotations;

namespace crudApplication.Models
{
    public class EditPassword
    {
        [Required]
        public String currentPassword { get; set; }="";
        [Required]
        public String newPassword { get; set; }="";
    }
}
