using System.ComponentModel.DataAnnotations;

namespace crudApplication.Models
{
    public class RegisterUser
    {
        [Required]
        public String UserName { get; set; } ="";
        [Required]
        public String FirstName { get; set; }="";
        [Required]
        public String LastName { get; set; } = "";
        [Required] 
        public String Password { get; set; } = "";
        [Required] 
        public String Email { get; set; } = "";
        public String PhoneNumber { get; set; } = "";
        


    }
}
