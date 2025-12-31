using crudApplication.Models;

public class SendEmail
{
    private readonly IConfiguration _configuration;

    public SendEmail(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string otp)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("api-key", _configuration["Brevo:ApiKey"]);

        var email = new BrevoEmailRequest
        {
            HtmlContent = $"<p>Hello,</p><p>Your OTP is <b>:{otp}</b></p>",
            Subject = "Your OTP Code",
            Sender = new Sender
            {
                Email = _configuration["Brevo:SenderEmail"],
                Name = _configuration["Brevo:SenderName"]
            },
            To = new List<Receiver>
            {
                new Receiver
                {
                    Email = "nishabhattarai778@gmail.com",
                    Name = "Nisha" // simple name
                }
            }
        };

        var response = await client.PostAsJsonAsync(
            "https://api.brevo.com/v3/smtp/email",
            email
        );

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Brevo Email Error: {error}");
        }
    }
}
