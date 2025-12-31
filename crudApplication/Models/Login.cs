using System.ComponentModel.DataAnnotations;

namespace crudApplication.Models
{
    public class Login
    {
        [Required]
        public String UserName { get; set; }=string.Empty;
        [Required]
        public String Password { get; set; }=string.Empty;
    }
}
