using crudApplication.Models;
using System.Text.Json;

namespace crudApplication.Api.IntegrationTest.Model
{
    public class MessageMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        public MessageMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
       public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Session.Keys.Contains("Otp"))
            {
                var otpData = new otpGenerator
                {
                    OTP = "1234",
                    Expiry = DateTime.UtcNow.AddMinutes(10),
                    user = new RegisterUsers
                    {
                        Email = "otp@test.com",
                        FirstName = "Otp",
                        LastName = "User",
                        Password = "Otp@12345",
                        UserName = "otpuser"
                    }
                };
                context.Session.SetString("Otp",JsonSerializer.Serialize(otpData));

            }
            await requestDelegate(context);
        }
    }
}
