using System.ComponentModel.DataAnnotations;

namespace crudApplication.Models
{
    public class Login
    {
        [Required]
        public String UserName { get; set; }="";
        [Required]
        public String Password { get; set; }="";
    }
}
