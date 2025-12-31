using System.ComponentModel.DataAnnotations;

namespace crudApplication.Models
{
    public class VerifyOtp
    {
        [Required]
        public string Otp { get; set; } = "";
    }
}
