namespace crudApplication.Models
{
    public class BrevoEmailRequest
    {
        public string HtmlContent { get; set; }
        public Sender Sender { get; set; }
        public string Subject { get; set; }
        public List<Receiver> To { get; set; }
    }

    public class Sender
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class Receiver
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

}
