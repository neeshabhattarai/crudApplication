using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace crudApplication.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public String FirstName { get; set; } = "";
        [Required]
        public String LastName { get; set; } = "";
        public String Address { get; set; } = "";
    }
}
