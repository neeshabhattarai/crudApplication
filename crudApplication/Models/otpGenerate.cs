namespace crudApplication.Models
{
    public class otpGenerate
    {
        public string OTP { get; set; } = "";
        public RegisterUser user { get; set; }
        public DateTime Expiry { get; set; }
    }
}
