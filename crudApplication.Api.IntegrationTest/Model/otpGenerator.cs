namespace crudApplication.Api.IntegrationTest.Model
{
    public class otpGenerator
    {
        public String OTP { get; set; }="";

        public DateTime Expiry {  get; set; }=DateTime.Now;

        public RegisterUsers user { get; set; }=new RegisterUsers();
    }
}
