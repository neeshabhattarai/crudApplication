using System.ComponentModel.DataAnnotations;

namespace crudApplication.Models
{
    public class EditProfile
    {
        [Required(ErrorMessage ="FirstName should be filled")]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "LastName should be filled")]

        public string LastName { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";
    }
}
